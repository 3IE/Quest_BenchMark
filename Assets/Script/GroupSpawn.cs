using System;
using UnityEngine;

namespace Script
{
    public class GroupSpawn : MonoBehaviour
    {
        private ToolGun _toolGun;
        
        public uint nbObjects;
        public float distanceBetweenObjects;
        public float distanceFromPlayer;
        public float height;

        private void Awake() {
            _toolGun = FindObjectOfType<ToolGun>();
            
            if (distanceBetweenObjects < 0 || distanceFromPlayer < 0 || height < 0)
                throw new ArgumentException(
                    "Distance between objects, distance from player or height are negative");
        }
        
        private Vector3 RotatePointAroundPivotByYRotation(Vector3 point, Vector3 pivot, float angle) {
            var dir = point - pivot; // get point direction relative to pivot
            dir = Quaternion.Euler(0, angle, 0) * dir; // rotate it
            point = dir + pivot; // calculate rotated point
            return point; // return it
        }
        
        public void SpawnGroup()
        {
            uint squareSide = (uint) Mathf.Ceil(Mathf.Sqrt(nbObjects));
            uint squareSideHalf = squareSide / 2; 
            var transformPosition = Camera.main!.transform.position;
            
            //Rotation on the Y axis of the Camera in degree
            var rotationY = Camera.main!.transform.eulerAngles.y;
            
            uint n = 0;
            for (uint i = 0; n < nbObjects; i++) {
                for (int j = 0; n < nbObjects && j < squareSide; j++, n++) {
                    // I might want to have this depend on the ItemScale of the object too
                    var defaultPosition = new Vector3(
                        transformPosition.x - distanceBetweenObjects * j + distanceBetweenObjects * squareSideHalf,
                        height, 
                        transformPosition.z + distanceFromPlayer + distanceBetweenObjects * i);
                    
                    var rotatedPosition = RotatePointAroundPivotByYRotation(defaultPosition,
                        new Vector3(transformPosition.x, height, transformPosition.z), rotationY);
                    
                    _toolGun.ToolCreate(rotatedPosition);
                }
            }
        }
    }
}
