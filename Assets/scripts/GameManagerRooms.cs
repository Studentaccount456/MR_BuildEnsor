using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RoomCompletionStatus roomStatus;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}