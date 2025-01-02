using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Id_05.Scripts
{
    public class UIController : MonoBehaviour
    {
        public RoomCompletionStatus roomStatus;
        
        [Tooltip("List of objects to toggle visibility.")]
        public List<GameObject> toggleableObjects = new();

        private int currentToggleIndex;

        public void QuitGame()
        {
            Application.Quit();
        }

        public void RestartGame()
        {
            roomStatus.room1Completed = false;
            roomStatus.room2Completed = false;
            roomStatus.room3Completed = false;
            roomStatus.room4Completed = false;
            
            SceneManager.LoadScene("Main scene");
        }

        public void ToggleObject()
        {
            if (toggleableObjects.Count == 0) return;

            GameObject obj = toggleableObjects[currentToggleIndex];
            if (obj)
            {
                obj.SetActive(!obj.activeSelf);
            }
            
            currentToggleIndex = (currentToggleIndex + 1) % toggleableObjects.Count;
        }
    }
}