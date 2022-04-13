using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Script
{
    public class MeshList : MonoBehaviour
    {
        public static List<Mesh> GetMeshList()
        {
            // get the mesh folder in CurrentDirectory or create it
            if (Application.isEditor)
                return new List<Mesh>( Resources.LoadAll<Mesh>("Default"));
            
            string path = Path.Combine(Application.persistentDataPath, "Mesh");
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                if (files.Length == 0)
                    return new List<Mesh>(Resources.LoadAll<Mesh>("Default"));
                foreach (var file in files)
                    File.Copy(file,
                        $"{Application.dataPath}/Resources/Mesh/{Path.GetFileName(file)}",
                        true);
                return new List<Mesh>(Resources.LoadAll<Mesh>("Mesh"));
            }
            Directory.CreateDirectory(path); 
            return new List<Mesh>(Resources.LoadAll<Mesh>("Default"));
        }
    }
}
