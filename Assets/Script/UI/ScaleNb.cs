using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class ScaleNb : MonoBehaviour
    {
        [SerializeField] private ToolGun _toolGun;
        private TextMeshProUGUI m_Text;

        private void Start()
        {
            m_Text = GetComponent<TextMeshProUGUI>();
            m_Text.text = $"{_toolGun.ItemScale}";
        }
        
        public void Increment()
        {
            _toolGun.ItemScale++;
            m_Text.text = $"{_toolGun.ItemScale}";
        }

        public void Decrement()
        {
            //ItemScale cannot go below 1
            if (_toolGun.ItemScale <= 1.0f) return;
            
            _toolGun.ItemScale--;
            m_Text.text = $"{_toolGun.ItemScale}";
        }
    }
}