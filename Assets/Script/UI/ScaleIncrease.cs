using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.UI
{
    public class ScaleIncrease : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private ScaleNb ScaleNb;
        private bool _isPressed;

        public void OnPointerDown(PointerEventData eventData) {
            _isPressed = true;
            StartCoroutine(PressHold());
        }
        
        public void OnPointerUp(PointerEventData eventData)
            => _isPressed = false;

        private IEnumerator PressHold()
        {
            ScaleNb.Increment();

            yield return new WaitForSeconds(0.5f);
            while (_isPressed) {
                ScaleNb.Increment();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
