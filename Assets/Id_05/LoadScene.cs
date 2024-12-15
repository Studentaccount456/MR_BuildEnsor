using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LoadScene : MonoBehaviour
{
    [System.Serializable]
    public class MaskScenePair
    {
        public GameObject maskObject; // Het masker-object
        public string sceneName;      // De naam van de bijbehorende scène
    }

    [Header("Mask to Scene Configuration")]
    public MaskScenePair[] maskScenePairs; // Lijst met mask-objecten en hun scènes
    public Transform playerHead;          // Transform van de speler (meestal de VR-camera)
    public float activationDistance = 0.5f; // Afstand waarop de scène verandert

    private void Update()
    {
        foreach (MaskScenePair pair in maskScenePairs)
        {
            if (pair.maskObject != null && playerHead != null)
            {
                float distance = Vector3.Distance(pair.maskObject.transform.position, playerHead.position);

                if (distance <= activationDistance)
                {
                    Debug.Log($"Masker {pair.maskObject.name} dichtbij! Laden van scène: {pair.sceneName}");
                    ChangeScene(pair.sceneName);
                    return; // Stop na het laden van één scène
                }
            }
        }
    }

    private void ChangeScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Geen scène ingesteld voor dit masker!");
        }
    }
}