using UnityEngine;

public class MaskCameraController : MonoBehaviour
{
    [Header("Mask and Camera Settings")]
    public Transform maskTransform;   // The mask's transform in the VR environment
    public Camera sceneCamera;        // The camera in the loaded scene

    [Header("Rotation Restriction")]
    public Vector3 rotationLimits = new(30f, 30f, 0f); // Restrict rotation (pitch, yaw, roll)

    private Quaternion lastMaskRotation;  // Tracks the mask's rotation from the last frame
    private Quaternion cameraRotation;    // Tracks the cumulative rotation of the camera

    void Start()
    {
        if (maskTransform == null || sceneCamera == null)
        {
            Debug.LogError("Mask transform or Scene camera is not assigned.");
            return;
        }

        // Initialize lastMaskRotation to the current rotation
        lastMaskRotation = maskTransform.rotation;

        // Start with the camera's current rotation
        cameraRotation = sceneCamera.transform.rotation;
    }

    void Update()
    {
        if (!maskTransform || !sceneCamera) return;

        // Calculate the rotation delta (difference) since the last frame
        Quaternion deltaRotation = Quaternion.Inverse(lastMaskRotation) * maskTransform.rotation;

        // Apply the rotation delta directly to the camera
        cameraRotation *= deltaRotation;

        // Clamp the resulting rotation angles
        Vector3 clampedEuler = cameraRotation.eulerAngles;
        clampedEuler.x = ClampAngle(clampedEuler.x, -rotationLimits.x, rotationLimits.x); // Pitch
        clampedEuler.y = ClampAngle(clampedEuler.y, -rotationLimits.y, rotationLimits.y); // Yaw
        clampedEuler.z = ClampAngle(clampedEuler.z, -rotationLimits.z, rotationLimits.z); // Roll (optional)

        // Apply the clamped rotation back to the camera
        sceneCamera.transform.rotation = Quaternion.Euler(clampedEuler);

        // Update the last mask rotation for the next frame
        lastMaskRotation = maskTransform.rotation;
    }

    // Helper method to clamp angles
    private float ClampAngle(float angle, float min, float max)
    {
        // Normalize the angle to the range [0, 360]
        angle %= 360;
        if (angle < 0) angle += 360;

        // Clamp the angle to the range [min, max]
        return Mathf.Clamp(angle, min, max);
    }

}
