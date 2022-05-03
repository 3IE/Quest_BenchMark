using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.UI
{
    public class GroupIncrease : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private GroupNb groupNb;
        private bool _isPressed;

        private void Start()
            => groupNb = GetComponentInParent<GroupNb>();

        public void OnPointerDown(PointerEventData eventData) {
            _isPressed = true;
            StartCoroutine(PressHold());
        }
        
        public void OnPointerUp(PointerEventData eventData)
            => _isPressed = false;

        private IEnumerator PressHold()
        {
            groupNb.Increment();

            yield return new WaitForSeconds(0.5f);
            while (_isPressed) {
                groupNb.Increment();
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}