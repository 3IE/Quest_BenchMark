using Unity.XR.CoreUtils;
using UnityEngine;

namespace Script
{
    public class GroupSpawn : MonoBehaviour
    {
        private ToolGun _toolGun;
        private XROrigin _origin;
        public uint nbObjects;
        public uint distanceBetweenObjects;
        public uint distanceFromPlayer;
        public uint height;

        private void Awake() {
            _toolGun = FindObjectOfType<ToolGun>();
            _origin = FindObjectOfType<XROrigin>();
        }
        
        public void SpawnGroup()
        {
            uint squareSide = (uint) Mathf.Ceil(Mathf.Sqrt(nbObjects));
            uint squareSideHalf = squareSide / 2;
            uint n = 0;
            for (uint i = 0; n < nbObjects; i++) {
                for (int j = 0; n < nbObjects && j < squareSide; j++, n++) {
                    // TO TEST
                    var defaultPosition = new Vector3(
                        _origin.transform.position.x - distanceBetweenObjects * j + distanceBetweenObjects * squareSideHalf,
                        _origin.transform.position.y + height, 
                        _origin.transform.position.z + distanceFromPlayer + distanceBetweenObjects * i);
                    
                    var spawnPosition = Vector3.RotateTowards(defaultPosition,
                        _origin.transform.forward, Mathf.PI * 2, 0);
                    
                    Instantiate(_toolGun.Selected, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
