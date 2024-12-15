using UnityEngine;

public class RoomCompletionTracker : MonoBehaviour
{
    // ScriptableObject Reference
    public RoomCompletionStatus roomStatus;
    public int roomNumber;

    public void CompleteRoom()
    {
        switch (roomNumber)
        {
            case 1: roomStatus.room1Completed = true; break;
            case 2: roomStatus.room2Completed = true; break;
            case 3: roomStatus.room3Completed = true; break;
            case 4: roomStatus.room4Completed = true; break;
        }
    }
}
