using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.UI
{
    public class SelectMesh : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TextMeshProUGUI ButtonText;
        private ToolGun _modelList;
        private int model;
        private void OnEnable()
            => _modelList = FindObjectOfType<ToolGun>();
        public void SetText(string modelName, int index)
        {
            ButtonText.text = modelName;
            model = index;
            if (index == 0)
                GetComponent<Button>().Select();
        }

        public void OnPointerDown(PointerEventData eventData)
            => _modelList.ToolSelect(model);
    }
}
