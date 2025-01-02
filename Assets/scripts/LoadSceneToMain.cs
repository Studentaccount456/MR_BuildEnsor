using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class LoadSceneToMain : MonoBehaviour
{
    [Header("Scene Configuration")]
    public string sceneName;

    public XRGrabInteractable targetObject;
    private XRGrabInteractable grabInteractable;

    public RoomCompletionStatus roomStatus;
    public int roomNumber;

    private void Awake()
    {
        grabInteractable = targetObject != null ? targetObject : GetComponent<XRGrabInteractable>();

        if (grabInteractable == null)
        {
            return;
        }

        grabInteractable.selectEntered.AddListener(OnObjectGrabbed);
    }

    private void OnDestroy()
    {
        // Remove listener to avoid memory leaks
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnObjectGrabbed);
        }
    }

    private void OnObjectGrabbed(SelectEnterEventArgs args)
    {
        MarkRoomAsCompleted();
        ChangeScene(sceneName);
    }

    private void MarkRoomAsCompleted()
    {
        if (roomStatus == null)
        {
            return;
        }

        // Update the completion status based on the room number
        switch (roomNumber)
        {
            case 1: roomStatus.room1Completed = true; break;
            case 2: roomStatus.room2Completed = true; break;
            case 3: roomStatus.room3Completed = true; break;
            case 4: roomStatus.room4Completed = true; break;
        }
    }

    private void ChangeScene(string nameOfScene)
    {
        if (roomStatus.room1Completed && roomStatus.room2Completed && roomStatus.room3Completed &&
            roomStatus.room4Completed)
        {
            SceneManager.LoadScene("Ending Scene");
        }
        else
        {
            if (!string.IsNullOrEmpty(nameOfScene))
            {
                SceneManager.LoadScene(nameOfScene);
            }
        }
    }
}
