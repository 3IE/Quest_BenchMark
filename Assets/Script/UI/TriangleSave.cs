using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.UI
{
    public class TriangleSave : MonoBehaviour, IPointerDownHandler
    { // Unused
        [SerializeField] private TMP_Text m_Text;
        private ToolGun _toolGun;

        private void Start()
            => _toolGun = FindObjectOfType<ToolGun>();

        public void OnPointerDown(PointerEventData eventData)
            => m_Text.text += $"\n{_toolGun.triangleNb}";
    }
}
