using UnityEngine;

[CreateAssetMenu(fileName = "RoomCompletionStatus", menuName = "Game/RoomCompletionStatus")]
public class RoomCompletionStatus : ScriptableObject
{
    public bool room1Completed;
    public bool room2Completed;
    public bool room3Completed;
    public bool room4Completed;

    private void OnEnable()
    {
        room1Completed = false;
        room2Completed = false;
        room3Completed = false;
        room4Completed = false;
    }
}