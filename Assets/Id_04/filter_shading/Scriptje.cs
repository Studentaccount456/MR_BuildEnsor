using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShaderEffect : MonoBehaviour
{
    public Shader customShader; // Drag and drop your shader here in the Inspector.
    private Material effectMaterial;

    private void Start()
    {
        if (customShader == null || !customShader.isSupported)
        {
            Debug.LogError("Shader is not assigned or not supported!");
            enabled = false; // Disable the script if the shader doesn't work.
            return;
        }

        // Create a material from the shader
        effectMaterial = new Material(customShader);
        Debug.Log("Material created successfully.");

        // Check camera setup
        Camera camera = GetComponent<Camera>();
        if (camera != null)
        {
            Debug.Log("Camera is correctly attached: " + camera.name);
        }
        else
        {
            Debug.LogError("Camera component not found!");
        }
    }


    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Debug.Log("OnRenderImage is being called.");

        if (effectMaterial != null)
        {
            Debug.Log("Applying shader effect...");
            Graphics.Blit(src, dst, effectMaterial);
        }
        else
        {
            // Fallback to the default rendering
            Debug.Log("Fallback basic rendering...");
            Graphics.Blit(src, dst);
        }
    }
}
