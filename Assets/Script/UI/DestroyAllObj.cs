using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.UI
{
    public class DestroyAllObj : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private ToolGun _toolGun;

        public void OnPointerDown(PointerEventData eventData)
            => _toolGun.ToolDestroyAll();
    }
}