using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class Cursor3D : MonoBehaviour
    {
        public const float speed = 0.1f;
        public const float holdTime = 0.5f;
        public const float holdInterval = 0.1f;
        [SerializeField] private bool Logs;
        [SerializeField] private GameObject _origin;
        [SerializeField] private ToolGun _toolGun;
        private XRIDefaultInputActions _inputActions;

        private void Start()
        {
            _inputActions = new XRIDefaultInputActions();
            _inputActions.Cursor3D.Enable();
            _inputActions.Cursor3D.ButtonA.performed += PressA;
            _inputActions.Cursor3D.ButtonB.performed += PressB;
            _inputActions.Cursor3D.CursorReset.performed += CursorReset;
            
            transform.SetParent(_origin.transform);
            transform.SetPositionAndRotation(_origin.transform.position, _origin.transform.rotation);
            
            if (Logs) Debug.Log(
                $"Cursor:\n\tParent:{transform.parent.name}");
        }
        
        private GameObject TobeDestroyed;

        private void PressB(InputAction.CallbackContext obj)
            => StartCoroutine(HoldB(obj.action));

        private IEnumerator HoldB(InputAction inputAction)
        {
            _toolGun.ToolDestroy(TobeDestroyed);
            
            yield return new WaitForSeconds(0.7f);
            while (inputAction.IsPressed())
            {
                _toolGun.ToolDestroy(TobeDestroyed);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void PressA(InputAction.CallbackContext obj)
            => StartCoroutine(HoldA(obj.action));

        private IEnumerator HoldA(InputAction inputAction)
        {
            _toolGun.ToolCreate(transform.position);
            
            yield return new WaitForSeconds(0.7f);
            while (inputAction.IsPressed())
            {
                _toolGun.ToolCreate(transform.position);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void CursorReset(InputAction.CallbackContext obj)
            => transform.SetPositionAndRotation(
                _origin.transform.position, _origin.transform.rotation);

        private void FixedUpdate() => MoveCursor();

        private void MoveCursor()
        {
            var inputVector = _inputActions.Cursor3D.Cursor.ReadValue<Vector2>();
            
            //if (Logs) Debug.Log($"Cursor:\tMove:{inputVector}");
            
            transform.Translate(new Vector3(0,0, inputVector.y) * speed);
            
            //The cursor cannot go behind the hand
            if (transform.transform.localPosition.z < 0)
                transform.SetPositionAndRotation(
                    _origin.transform.position, _origin.transform.rotation);
        }

        private void OnTriggerStay(Collider other)
        {
            if (Logs) Debug.Log($"Cursor:\tEnter:{other.name}");
            TobeDestroyed = other.gameObject;
        }
        
        private void OnTriggerExit(Collider collision)
            => TobeDestroyed = null;

        /*
        private void OnEnable() => _inputActions.Cursor3D.Enable();
        private void OnDisable() => _inputActions.Cursor3D.Disable();
        */
    }
}
