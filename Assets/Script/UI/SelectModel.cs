using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
            Debug.Log(_text);
        }
        
        public void SetButton(string text, int i)
        {
            // Debug.Log($"$ {_text} {_text}");
            _text.text = text;
            index = i;
        }

        public void OnPointerDown(PointerEventData eventData)
            => toolGun.ToolSelect(index);
    }
}
