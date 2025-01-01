using UnityEngine;
using UnityEngine.SceneManagement;

namespace Id_05
{
    public class LoadScene : MonoBehaviour
    {
        [System.Serializable]
        public class MaskScenePair
        {
            public GameObject maskObject;
            public string sceneName;
        }

        [Header("Mask to Scene Configuration")]
        public MaskScenePair[] maskScenePairs;

        public Transform playerHead;
        public float activationDistance = 0.5f;

        private void Update()
        {
            foreach (MaskScenePair pair in maskScenePairs)
            {
                if (!pair.maskObject || !playerHead) continue;

                float distance = Vector3.Distance(pair.maskObject.transform.position, playerHead.position);

                if (!(distance <= activationDistance)) continue;

                ChangeScene(pair.sceneName);
                return;
            }
        }

        private void ChangeScene(string sceneName)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}