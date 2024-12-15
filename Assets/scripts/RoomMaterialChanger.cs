using UnityEngine;

public class RoomMaterialChanger : MonoBehaviour
{
    // ScriptableObject reference
    public RoomCompletionStatus roomStatus;
    public Material[] materialsListBinary;
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        UpdateMaterial();
    }

    public void UpdateMaterial()
    {
        bool[] completionStatus = new bool[]
        {
            roomStatus.room1Completed,
            roomStatus.room2Completed,
            roomStatus.room3Completed,
            roomStatus.room4Completed
        };

        int index = GetMaterialIndex(completionStatus);
        objectRenderer.material = materialsListBinary[index];
    }

    private int GetMaterialIndex(bool[] completionStatus)
    {
        // Convert boolean states to an index (e.g., 0000 = 0, 1000 = 8, etc.)
        int index = 0;
        for (int i = 0; i < completionStatus.Length; i++)
        {
            if (completionStatus[i])
                index += 1 << i; // Binary shift to calculate unique index
        }
        return index; // Should be between 0 and 15
    }
}
