using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Script
{
    public class ToolGun : MonoBehaviour
    {
        [SerializeField] private bool logs;
        public GameObject Selected;
        private int indexSelected;
        
        public GameObject copy;

        private long objNb;
        public ulong triangleNb;
        
        [SerializeField] private TMP_Text objNb_text;
        [SerializeField] private TMP_Text triangleNb_text;

        //private RefreshList refreshList;
        public GameObject[] modelList;
        private List<GameObject> simpleModelList; // modelList with object non rigidbody
        private List<uint> triangleList; // list of triangles of each model


        [SerializeField] private float itemScale;
        [SerializeField] private bool rigidBodyOn;
        public float ItemScale {
            get => itemScale;
            set {
                itemScale = value; 
                Selected.transform.localScale = new Vector3(itemScale, itemScale, itemScale);
            }
        }
        public bool RigidBodyOn {
            set
            {
                rigidBodyOn = value;
                Selected = rigidBodyOn ? modelList[indexSelected] : simpleModelList[indexSelected];
                ItemScale = itemScale;
            }
        }
        public List<uint> TriangleList => triangleList;

        private void Awake() {
            
            OVRPlugin.systemDisplayFrequency = 90.0f;
            
            simpleModelList = new List<GameObject>(); // make a list of copy of the modelList
            triangleList = new List<uint>();
            foreach (var model in modelList)
            {
                var triangleCount = checked((uint)model.GetComponent<MeshFilter>().sharedMesh.triangles.Length / 3);
                
                //Add a component to each model and sets its triangleCount
                var component = model.GetComponent<ModelCopy>();
                if (component == null)
                    component = model.AddComponent<ModelCopy>();
                component.Set(triangleCount, this);
                
                TriangleList.Add(triangleCount);
                
                // Set the List of model without rigidBodies
                var modelCopy = Instantiate(model);
                modelCopy.SetActive(false);
                if (modelCopy.GetComponent<XRGrabInteractable>() != null)
                    Destroy(modelCopy.GetComponent<XRGrabInteractable>());
                if (modelCopy.GetComponent<Rigidbody>() != null)
                    Destroy(modelCopy.GetComponent<Rigidbody>());
                simpleModelList.Add(modelCopy); 
            }
            
            objNb = 0;
            ItemScale = itemScale;
        }

        private void Start()
        {
            ////if (logs) Debug.Log($"{modelList.Count} models loaded");
            
            //Setup of the duplicated object
            Selected = modelList[0]; // possibly null
            indexSelected = 0;
            ToolSelect(0);
            UpdateText();
        }

        public void ToolSelect(int model)
        { // Switch the GameObject Selected
            Selected = modelList[model];
            indexSelected = model;
            ItemScale = itemScale;
            RigidBodyOn = rigidBodyOn;
            if (logs) Debug.Log($"Selected {Selected.name}");
        }
        public void ToolCreate(Vector3 position)
        {
            if (logs) Debug.Log($"Create: {Selected.name}");
            var instance = Instantiate(Selected, position, Quaternion.identity, copy.transform);
            if (!instance.activeSelf)
                instance.SetActive(true);
            UpdateText(objNbUpdate.Increment);
        }
        public void ToolDestroy(GameObject obj)
        {
            if (obj == null) {
                if (logs) Debug.Log("Destroy: Nothing selected");
            }
            else {
                if (logs) Debug.Log($"Destroy: {obj.name}");
                
                Destroy(obj);
                // No need to updateText as the object destroyed does it when it dies
            }
        }
        public void ModelDestroyed(uint triangle) // Called when a model is destroyed
            => UpdateText(objNbUpdate.Decrement, triangle);

        public void ToolDestroyAll() // Destroy all the objects children to the copy
        {
            if (logs) Debug.Log("Destroy all");
            foreach (Transform child in copy.transform)
                Destroy(child.gameObject);
        }
        
        private enum objNbUpdate {
            Neutral,
            Increment,
            Decrement,
            Reset
        }
        private void UpdateText(objNbUpdate update = objNbUpdate.Neutral, ulong triangleNbUp = 0)
        {
            switch (update)
            {
                case objNbUpdate.Increment:
                    objNb++;
                    triangleNb += TriangleList[indexSelected];
                    break;
                case objNbUpdate.Decrement:
                    objNb--;
                    triangleNb -= triangleNbUp;
                    break;
                case objNbUpdate.Reset:
                    objNb = 0;
                    triangleNb = 0;
                    break;
                case objNbUpdate.Neutral:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(update), update, null);
            }
            objNb_text.text = $"Objs: {objNb}";
            triangleNb_text.text = $"Tris: {triangleNb}";
        }
    }
}