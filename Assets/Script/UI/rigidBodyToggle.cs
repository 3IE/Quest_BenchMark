using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class rigidBodyToggle : MonoBehaviour
    {
        private Toggle toggle;
        private ToolGun toolGun;
        
        private void Start()
        {
            toggle = GetComponent<Toggle>();
            toolGun = FindObjectOfType<ToolGun>();
            OnToggle();
        }

        public void OnToggle()
        {
            toolGun.RigidBodyOn = toggle.isOn;
        }
    }
}