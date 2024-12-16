using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ToggleVisibilityOnGrab : MonoBehaviour
{
    [Tooltip("List of objects to toggle visibility.")]
    public List<GameObject> toggleableObjects = new List<GameObject>();

    private void OnEnable()
    {
        // Subscribe to the XR Grab Interactable events
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the XR Grab Interactable events
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (toggleableObjects.Count == 0)
        {
            return;
        }

        // Toggle the active state of each GameObject
        foreach (GameObject obj in toggleableObjects)
        {
            if (obj != null)
            {
                bool isActive = obj.activeSelf;
                obj.SetActive(!isActive);
            }
        }
    }
}
