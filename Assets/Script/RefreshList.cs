using System.Collections.Generic;
using Script.UI;
using UnityEngine;

namespace Script
{
    public class RefreshList : MonoBehaviour
    { // Not in use, as the Scrolling list given by Unity breaks in the Build
        public bool logs;
        private GameObject[] modelList;
        [SerializeField] private GameObject buttonTemplate;
        [SerializeField] private GameObject buttonParentTemplate;
        [SerializeField] private Transform ViewportTransform;
        //[SerializeField] private ScrollRect scrollRect;
        private GameObject buttonParent;
        
        private void Awake()
            => modelList = GetComponent<ToolGun>().modelList;
        
        public void Refresh()
        {
            Debug.Log("Refresh");
            string log = string.Empty;
            if (logs) log = $"List of models collected: (size: {modelList.Length})";
            
            if (buttonParent != null)
                Destroy(buttonParent);
            
            buttonParent = Instantiate(buttonParentTemplate, ViewportTransform);
            //scrollRect.content = buttonParent.GetComponent<RectTransform>();
            
            int i = 0;
            foreach (var model in modelList)
            {
                if (logs) log += $"\n\t{model.name}";
                var button = Instantiate(buttonTemplate,
                    buttonParent.transform, false);
                
                if (button != null) button.SetActive(true);
                button.GetComponent<SelectMesh>().SetText(model.name, i);
                i++;
            }
            if (logs) Debug.Log(log);
        }
    }
}
