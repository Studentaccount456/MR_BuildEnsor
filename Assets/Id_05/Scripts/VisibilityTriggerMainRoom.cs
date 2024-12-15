using System.Collections.Generic;
using UnityEngine;

public class ToggleVisibilityOnRoomStatus : MonoBehaviour
{
    [Tooltip("Objects to toggle visibility when Room 1 is completed.")]
    public List<GameObject> room1Objects = new List<GameObject>(); // Visible if not completed
    public List<GameObject> room1ObjectsInitiallyInvisible = new List<GameObject>(); // Start invisible, visible if not completed

    [Tooltip("Objects to toggle visibility when Room 2 is completed.")]
    public List<GameObject> room2Objects = new List<GameObject>(); // Visible if not completed
    public List<GameObject> room2ObjectsInitiallyInvisible = new List<GameObject>(); // Start invisible, visible if not completed

    [Tooltip("Objects to toggle visibility when Room 3 is completed.")]
    public List<GameObject> room3Objects = new List<GameObject>(); // Visible if not completed
    public List<GameObject> room3ObjectsInitiallyInvisible = new List<GameObject>(); // Start invisible, visible if not completed

    [Tooltip("Objects to toggle visibility when Room 4 is completed.")]
    public List<GameObject> room4Objects = new List<GameObject>(); // Visible if not completed
    public List<GameObject> room4ObjectsInitiallyInvisible = new List<GameObject>(); // Start invisible, visible if not completed

    public RoomCompletionStatus roomStatus; // Reference to your RoomCompletionStatus ScriptableObject

    private void Start()
    {
        // Ensure visibility is set based on the room completion status at the start
        SetVisibilityBasedOnRoomStatus();
    }

    private void SetVisibilityBasedOnRoomStatus()
    {
        // If the RoomCompletionStatus is null, return early
        if (roomStatus == null)
        {
            Debug.LogError("RoomCompletionStatus is not assigned!");
            return;
        }

        // Set visibility based on room completion status
        // Objects that start visible
        SetObjectVisibility(!roomStatus.room1Completed, room1Objects); // Visible if not completed
        SetObjectVisibility(!roomStatus.room2Completed, room2Objects); // Visible if not completed
        SetObjectVisibility(!roomStatus.room3Completed, room3Objects); // Visible if not completed
        SetObjectVisibility(!roomStatus.room4Completed, room4Objects); // Visible if not completed

        // Objects that start invisible
        SetObjectVisibility(roomStatus.room1Completed, room1ObjectsInitiallyInvisible); // Visible if not completed
        SetObjectVisibility(roomStatus.room2Completed, room2ObjectsInitiallyInvisible); // Visible if not completed
        SetObjectVisibility(roomStatus.room3Completed, room3ObjectsInitiallyInvisible); // Visible if not completed
        SetObjectVisibility(roomStatus.room4Completed, room4ObjectsInitiallyInvisible); // Visible if not completed
    }

    private void SetObjectVisibility(bool shouldBeVisible, List<GameObject> objectsToToggle)
    {
        // Iterate through each object in the list and set its active state
        foreach (GameObject obj in objectsToToggle)
        {
            if (obj != null)
            {
                obj.SetActive(shouldBeVisible); // Set to visible if true, invisible if false
            }
        }
    }
}
