using System;
using System.Collections;
using UnityEngine;

namespace Script
{
    public class Reticule : MonoBehaviour
    {
        private MeshRenderer mesh;
        private void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
        }

        public IEnumerator showUpCoroutine(Vector3 position)
        {
            transform.position = position;
            mesh.enabled = true;

            yield return new WaitForSeconds(1);

            mesh.enabled = false;
        }
    }
}
