using System.Collections.Generic;
using UnityEngine;

namespace Id_05.Scripts
{
    public class FinalMaskAttach : MonoBehaviour
    {
        [Tooltip("List of objects to toggle visibility.")]
        public List<GameObject> toggleableObjects = new();
        [Tooltip("List of objects to toggle shader.")]
        public List<GameObject> toggleableObjectsShader = new();
        [Tooltip("The shader the objects need to toggle to.")]
        public Shader shader;
        
        public GameObject maskObject;
        public Transform playerHead;
        public float activationDistance = 0.5f;
        
        private void Update()
        {
            if (!maskObject || !playerHead) return;

                float distance = Vector3.Distance(maskObject.transform.position, playerHead.position);

                if (!(distance <= activationDistance)) return;
                
                if (toggleableObjects.Count == 0)
                {
                    return;
                }
                
                RenderSettings.ambientIntensity = 1.0f;

                // Toggle the active state of each GameObject
                foreach (GameObject obj in toggleableObjects)
                {
                    if (!obj) continue;
                
                    bool isActive = obj.activeSelf;
                    obj.SetActive(!isActive);
                }
                
                // Apply the shader to the toggleableObjectsShader
                if (shader)
                {
                    foreach (GameObject obj in toggleableObjectsShader)
                    {
                        if (!obj) continue;

                        Renderer renderer = obj.GetComponent<Renderer>();
                        
                        if (!renderer) continue;
                        
                        foreach (var material in renderer.materials)
                        {
                            if (material.name.Contains("PaintingFrameMat")) continue;

                            material.shader = shader;
                        }
                    }
                }

                // Destroy the maskObject
                if (maskObject)
                {
                    Destroy(maskObject);
                }
            
                // Only trigger once
                Destroy(this);
        }
    }
}
