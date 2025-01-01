using UnityEngine;

namespace Id_04.Scripts
{
    public class FaceCamera : MonoBehaviour
    {
        public Transform cameraTransform;

        void Update()
        {
            if (!cameraTransform) return;
            
            // Calculate direction to the camera
            Vector3 directionToCamera = cameraTransform.position - transform.position;

            // Reverse the direction to make models face away from camera (Models are inversed)
            directionToCamera = -directionToCamera;

            // Lock the Y-axis to keep the object standing up (Otherwise they bend)
            directionToCamera.y = 0;

            // Calculate the base rotation
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

            // Apply an offset rotation (Model offset)
            Quaternion offsetRotation = Quaternion.Euler(0, -35, 0);

            // Combine the base rotation with the offset
            transform.rotation = targetRotation * offsetRotation;

        }
    }
}
