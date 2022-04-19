using UnityEngine;

namespace Script
{
    public class ModelCopy : MonoBehaviour
    {
        [SerializeField] private uint triangleCount;
        [SerializeField] private ToolGun toolGun;

        public void Set(uint triangleC, ToolGun toolG = null) {
            triangleCount = triangleC;
            if (toolG != null)
                toolGun = toolG;
            else
                FindObjectOfType<ToolGun>();
        }
        private void OnDestroy()
            => toolGun.ModelDestroyed(triangleCount);
    }
}
