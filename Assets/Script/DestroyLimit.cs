using UnityEngine;

namespace Script
{
    public class DestroyLimit : MonoBehaviour
    {
        private ToolGun _toolGun;
        
        private void Start()
            => _toolGun = FindObjectOfType<ToolGun>();

        private void OnCollisionStay(Collision collision)
            => _toolGun.ToolDestroy(collision.gameObject);
    }
}