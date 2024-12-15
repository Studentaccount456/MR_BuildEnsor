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
        ChangeScene(sceneName);
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
