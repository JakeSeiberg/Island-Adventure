using UnityEngine;

public class cutSceneCamera : MonoBehaviour
{
    public float sensitivity = 2f;

    private float yaw = 0f;   // left/right rotation
    private float pitch = 0f; // up/down rotation

    void Start()
    {
        // Lock the cursor to the game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Adjust yaw and pitch
        yaw += mouseX;
        pitch -= mouseY;

        // Clamp the values
        yaw = Mathf.Clamp(yaw, -90f, 90f);         // Left/right
        pitch = Mathf.Clamp(pitch, -60f, 20f);     // Up/down

        // Apply rotation
        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
