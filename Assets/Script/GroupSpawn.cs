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
        
        public void SpawnGroup()
        {
            uint squareSide = (uint) Mathf.Ceil(Mathf.Sqrt(nbObjects));
            uint squareSideHalf = squareSide / 2; 
            var transformPosition = Camera.main!.transform.position;
            
            //Rotation on the Y axis of the Camera in degree
            var rotationY = Camera.main!.transform.rotation.eulerAngles.y;
            
            Debug.Log($"GroupSpawn: forward '{rotationY}'");
            
            uint n = 0;
            for (uint i = 0; n < nbObjects; i++) {
                for (int j = 0; n < nbObjects && j < squareSide; j++, n++) {
                    // I might want to have this depend on the ItemScale of the object too
                    var defaultPosition = new Vector3(
                        transformPosition.x - distanceBetweenObjects * j + distanceBetweenObjects * squareSideHalf,
                        transformPosition.y + height, 
                        transformPosition.z + distanceFromPlayer + distanceBetweenObjects * i);
                    
                    //rotate the default position by player look forward
                    //var rotatedPosition = new Vector3(Mathf.Cos(-rotationY) - Mathf.Sin(-rotationY), 0, Mathf.Sin(-rotationY) + Mathf.Cos(-rotationY)) * defaultPosition;
                    
                    //Debug.Log($"GroupSpawn: default position '{defaultPosition}'\n\t\tspawn position '{rotatedPosition}'");
                    
                    _toolGun.ToolCreate(defaultPosition);
                }
            }
        }
    }
}
