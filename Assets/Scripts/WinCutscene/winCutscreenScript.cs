using UnityEngine;

public class WinCutsceneCamera : MonoBehaviour
{
    private float sensitivity = 2.0f;
    private float maxYawAngle = 130f; // 60 degrees left/right
    private float maxPitchAngle = 80f; // 80 degrees up/down (almost vertical)

    private float yaw;   // left/right
    private float pitch; // up/down

    private float initialYaw;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Record the starting yaw angle (Y axis rotation)
        initialYaw = transform.eulerAngles.y;
        yaw = 0f;
        pitch = 0f;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        yaw += mouseX;
        pitch -= mouseY;

        // Clamp yaw and pitch
        yaw = Mathf.Clamp(yaw, -maxYawAngle, maxYawAngle);
        pitch = Mathf.Clamp(pitch, -maxPitchAngle, maxPitchAngle);

        // Apply rotation relative to initial yaw
        transform.rotation = Quaternion.Euler(pitch, initialYaw + yaw, 0f);

        // Optional: Escape unlocks cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
