using UnityEngine;

namespace Script
{
    public class ModelCopy : MonoBehaviour
    {
        private uint triangleCount;
        private ToolGun toolGun;

        public void Set(uint triangleC, ToolGun toolG) {
            triangleCount = triangleC;
            toolGun = toolG;
        }
        private void OnDestroy()
            => toolGun.ModelDestroyed(triangleCount);
    }
}
