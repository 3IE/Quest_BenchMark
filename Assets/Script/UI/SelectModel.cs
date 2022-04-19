using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.UI
{
    public class SelectModel : MonoBehaviour, IPointerDownHandler
    {
        private TMP_Text _text;
        private ToolGun toolGun;
        [NonSerialized] public int index;

        private void Awake()
        {
            toolGun = FindObjectOfType<ToolGun>();
            _text = GetComponentInChildren<TMP_Text>();
        }
        
        public void SetButton(string text, int i)
        {
            _text.text = text;
            index = i;
        }

        public void OnPointerDown(PointerEventData eventData)
            => toolGun.ToolSelect(index);
    }
}
