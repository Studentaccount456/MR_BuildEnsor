using System.Collections.Generic;
using UnityEngine;

namespace Id_05.Scripts
{
    public class ToggleVisibilityOnRoomStatus : MonoBehaviour
    {
        // First list has the items that are visible when the room is not completed, and disappear on completion
        // Second list has the items that start invisible and appear on completion
        [Tooltip("Objects to toggle visibility when Room 1 is completed.")]
        public List<GameObject> room1Objects = new();
        public List<GameObject> room1ObjectsInitiallyInvisible = new();

        [Tooltip("Objects to toggle visibility when Room 2 is completed.")]
        public List<GameObject> room2Objects = new();
        public List<GameObject> room2ObjectsInitiallyInvisible = new();

        [Tooltip("Objects to toggle visibility when Room 3 is completed.")]
        public List<GameObject> room3Objects = new();
        public List<GameObject> room3ObjectsInitiallyInvisible = new();

        [Tooltip("Objects to toggle visibility when Room 4 is completed.")]
        public List<GameObject> room4Objects = new();
        public List<GameObject> room4ObjectsInitiallyInvisible = new();

        [Tooltip("Objects to toggle townfolk which only can appear after clearance of room 1 and 4.")]
        public List<GameObject> townfolkObjectsInitiallyInvisible = new();

        // Reference to RoomCompletionStatus ScriptableObject
        public RoomCompletionStatus roomStatus;

        private void Start()
        {
            SetVisibilityBasedOnRoomStatus();
        }

        private void SetVisibilityBasedOnRoomStatus()
        {
            // If the RoomCompletionStatus is null, return early
            if (roomStatus == null)
            {
                return;
            }

            // Objects that start visible
            SetObjectVisibility(!roomStatus.room1Completed, room1Objects);
            SetObjectVisibility(!roomStatus.room2Completed, room2Objects);
            SetObjectVisibility(!roomStatus.room3Completed, room3Objects);
            SetObjectVisibility(!roomStatus.room4Completed, room4Objects);

            // Objects that start invisible
            SetObjectVisibility(roomStatus.room1Completed, room1ObjectsInitiallyInvisible);
            SetObjectVisibility(roomStatus.room2Completed, room2ObjectsInitiallyInvisible);
            SetObjectVisibility(roomStatus.room3Completed, room3ObjectsInitiallyInvisible);
            SetObjectVisibility(roomStatus.room4Completed, room4ObjectsInitiallyInvisible);
            
            SetObjectVisibilitySpecial(roomStatus.room1Completed, roomStatus.room4Completed,
                townfolkObjectsInitiallyInvisible);
        }

        private void SetObjectVisibility(bool shouldBeVisible, List<GameObject> objectsToToggle)
        {
            foreach (GameObject obj in objectsToToggle)
            {
                if (obj != null)
                {
                    // Set to visible if true, invisible if false
                    obj.SetActive(shouldBeVisible);
                }
            }
        }

        private void SetObjectVisibilitySpecial(bool room1Cleared, bool shouldBeVisible,
            List<GameObject> objectsToToggle)
        {
            if (!room1Cleared)
            {
                return;
            }

            foreach (GameObject obj in objectsToToggle)
            {
                if (obj != null)
                {
                    // Set to visible if true, invisible if false
                    obj.SetActive(shouldBeVisible);
                }
            }
        }
    }
}