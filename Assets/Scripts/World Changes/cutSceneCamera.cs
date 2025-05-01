using UnityEngine;

public class CutSceneCamera : MonoBehaviour
{
    public float sensitivity = 2f;

    private float yaw = 0f;   
    private float pitch = 0f; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        yaw += mouseX;
        pitch -= mouseY;

        yaw = Mathf.Clamp(yaw, -90f, 90f);         
        pitch = Mathf.Clamp(pitch, -60f, 20f);    

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
