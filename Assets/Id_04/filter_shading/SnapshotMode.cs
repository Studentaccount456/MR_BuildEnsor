using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SnapshotMode : MonoBehaviour
{
    private Shader noneShader;
    private Shader greyscaleShader;
    private Shader sepiaShader;
    private Shader paintingShader;

    private List<SnapshotFilter> filters = new List<SnapshotFilter>();

    private int filterIndex = 0;

    private void Awake()
    {
        // Find all shader files and log their status.
        noneShader = Shader.Find("SMO/Complete/Base");
        //noneShader = Shader.Find("Unlit/NewUnlitShader");

        Debug.Log(noneShader != null ? "None shader loaded successfully." : "Failed to load None shader.");

        greyscaleShader = Shader.Find("SMO/Complete/Greyscale");
        Debug.Log(greyscaleShader != null ? "Greyscale shader loaded successfully." : "Failed to load Greyscale shader.");

        sepiaShader = Shader.Find("SMO/Complete/Sepia");
        Debug.Log(sepiaShader != null ? "Sepia shader loaded successfully." : "Failed to load Sepia shader.");

        paintingShader = Shader.Find("SMO/Complete/Painting");
        Debug.Log(paintingShader != null ? "Painting shader loaded successfully." : "Failed to load Painting shader.");

        // Create all filters.
        filters.Add(new BaseFilter("None", Color.white, noneShader));
        filters.Add(new BaseFilter("Greyscale", Color.white, greyscaleShader));
        filters.Add(new BaseFilter("Sepia Tone", new Color(1.00f, 1.00f, 0.79f), sepiaShader));
        filters.Add(new BaseFilter("Painting", Color.white, paintingShader));

        Debug.Log($"Total filters created: {filters.Count}");
    }

    private void Update()
    {
        var mouse = Mouse.current; // Access mouse using Input System.
        if (mouse == null)
        {
            Debug.LogError("Mouse is not detected!");
            return; // Make sure the mouse exists.
        }

        int lastIndex = filterIndex;

        // Logic to swap between filters and log interactions.
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Left mouse button pressed.");
            if (--filterIndex < 0)
            {
                filterIndex = filters.Count - 1;
            }
            Debug.Log($"Switched to filter index: {filterIndex} ({filters[filterIndex].name})");
        }
        else if (mouse.rightButton.wasPressedThisFrame)
        {
            Debug.Log("Right mouse button pressed.");
            if (++filterIndex >= filters.Count)
            {
                filterIndex = 0;
            }
            Debug.Log($"Switched to filter index: {filterIndex} ({filters[filterIndex].name})");
        }
    }

    // Delegate OnRenderImage() to a SnapshotFilter object.
    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (filters[filterIndex] == null)
        {
            Debug.LogError($"Filter at index {filterIndex} is null.");
            return;
        }

        Debug.Log($"Applying filter: {filters[filterIndex].name}");
        filters[filterIndex].OnRenderImage(src, dst);
    }
}
