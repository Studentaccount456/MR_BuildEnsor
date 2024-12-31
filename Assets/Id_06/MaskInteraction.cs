using UnityEngine;
using UnityEngine.SceneManagement;

public class MaskInteraction : MonoBehaviour
{
    public string sceneToLoad;

    void OnMaskPutOn()
    {
        // Start loading the new scene
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        // Optionally disable the mask object once the scene is loaded
    }
}