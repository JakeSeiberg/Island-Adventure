using UnityEngine;

public class WinCutsceneCamera : MonoBehaviour
{
    private float sensitivity = 2.0f;
    private float maxYawAngle = 130f; 
    private float maxPitchAngle = 80f; 

    private float yaw;   
    private float pitch; 

    private float initialYaw;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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

        yaw = Mathf.Clamp(yaw, -maxYawAngle, maxYawAngle);
        pitch = Mathf.Clamp(pitch, -maxPitchAngle, maxPitchAngle);

        transform.rotation = Quaternion.Euler(pitch, initialYaw + yaw, 0f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
