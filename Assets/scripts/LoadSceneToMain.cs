using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class LoadSceneToMain : MonoBehaviour
{
    [Header("Scene Configuration")]
    public string sceneName; // De naam van de bijbehorende scène

    public XRGrabInteractable targetObject; // Het specifieke object dat moet worden gegrabt
    private XRGrabInteractable grabInteractable;

    public RoomCompletionStatus roomStatus; // Reference to the ScriptableObject
    public int roomNumber; // The room number this script is responsible for (1, 2, 3, or 4)

    private void Awake()
    {
        // Zorg ervoor dat er een XRGrabInteractable-component op dit object staat
        if (targetObject != null)
        {
            grabInteractable = targetObject;
        }
        else
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }

        if (grabInteractable == null)
        {
            Debug.LogError("Geen XRGrabInteractable gevonden! Zorg ervoor dat een doelobject is toegewezen of dat dit object een XRGrabInteractable-component heeft.");
            return;
        }

        // Voeg de gebeurtenislistener toe voor wanneer het object wordt gegrabt
        grabInteractable.selectEntered.AddListener(OnObjectGrabbed);
    }

    private void OnDestroy()
    {
        // Verwijder de listener om geheugenlekken te voorkomen
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
            Debug.LogError("RoomCompletionStatus ScriptableObject is not assigned!");
            return;
        }

        // Update the completion status based on the room number
        switch (roomNumber)
        {
            case 1: roomStatus.room1Completed = true; break;
            case 2: roomStatus.room2Completed = true; break;
            case 3: roomStatus.room3Completed = true; break;
            case 4: roomStatus.room4Completed = true; break;
            default:
                Debug.LogError("Invalid room number! Must be between 1 and 4.");
                break;
        }

        Debug.Log($"Room {roomNumber} marked as completed.");
    }

    private void ChangeScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Geen scène ingesteld voor dit object!");
        }
    }
}
