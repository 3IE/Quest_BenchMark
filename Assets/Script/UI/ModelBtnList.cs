using System.Collections.Generic;
using UnityEngine;

namespace Script.UI
{
    public class ModelBtnList : MonoBehaviour
    {
        private List<uint> triangleList;
        public SelectModel[] btnList;

        private void Awake()
            => triangleList = FindObjectOfType<ToolGun>().TriangleList;

        private void Start()
        {
            var length = btnList.Length;
            for (int b = 0; b < length; b++)
            {
                // Get the number of triangle in the model's mesh
                var trianglesNb = triangleList[b];
                
                if (Application.isEditor) Debug.Log($"button[{b}]: {trianglesNb} triangles");
                
                btnList[b].SetButton(trianglesNb.ToString(), b);
            }
        }
    }
}
