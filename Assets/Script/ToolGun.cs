using System;
using TMPro;
using UnityEngine;

namespace Script
{
    public class ToolGun : MonoBehaviour
    {
        [SerializeField] private bool logs;
        [SerializeField] private GameObject Selected;
        
        private GameObject copy;

        private long objNb;
        [SerializeField] private TMP_Text objNb_text;

        private RefreshList refreshList;
        public GameObject[] modelList;
        
        [SerializeField] private float itemScale;
        public float ItemScale {
            get => itemScale;
            set {
                itemScale = value; 
                Selected.transform.localScale = new Vector3(itemScale, itemScale, itemScale);
            }
        }

        private void Awake() {
            refreshList = GetComponent<RefreshList>();
            logs = Application.isEditor && logs;
            objNb = 0;
            ItemScale = itemScale;
            copy = Instantiate(new GameObject("copy"), Vector3.zero, Quaternion.identity);
        }

        private void Start()
        {
            //if (logs) Debug.Log($"{modelList.Count} models loaded");
            // modelList.Sort( (a, b) => a.name.CompareTo(b.name));
            
            refreshList.Refresh();
            
            //Setup of the duplicated object
            Selected = modelList[0]; // possibly null
            ToolSelect(0);
            UpdateText();
        }

        public void ToolSelect(int model)
        { // Switch the GameObject Selected
            Selected = modelList[model];
            if (logs) Debug.Log($"Selected {Selected.name}");
        }
        public void ToolCreate(Vector3 position)
        {
            if (logs) Debug.Log($"Create: {Selected.name}");
            Instantiate(Selected, position, Quaternion.identity, copy.transform);
            UpdateText(objNbUpdate.Increment);
        }
        public void ToolDestroy(GameObject obj)
        {
            if (obj == null) {
                if (logs) Debug.Log("Destroy: Nothing selected");
            }
            else {
                if (logs) Debug.Log($"Destroy: {obj.name}");
                Destroy(obj);
                UpdateText(objNbUpdate.Decrement);
            }
        }
        // Destroy all the objects children to the copy
        public void ToolDestroyAll()
        {
            if (logs) Debug.Log("Destroy all");
            foreach (Transform child in copy.transform)
                Destroy(child.gameObject); 
            UpdateText(objNbUpdate.Reset);
        }
        
        private enum objNbUpdate {
            Neutral,
            Increment,
            Decrement,
            Reset
        }
        private void UpdateText(objNbUpdate update = objNbUpdate.Neutral)
        {
            switch (update)
            {
                case objNbUpdate.Increment:
                    objNb++;
                    break;
                case objNbUpdate.Decrement:
                    objNb--;
                    break;
                case objNbUpdate.Reset:
                    objNb = 0;
                    break;
                case objNbUpdate.Neutral:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(update), update, null);
            }
            objNb_text.text = $"Objs: {objNb}";
        }
    }
}