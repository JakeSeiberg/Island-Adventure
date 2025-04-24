using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform playerPos;

    float xRotation;
    float yRotation;

    public static Vector3 currentRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 initialLookRotation = playerData.playerRotation;

        transform.rotation = Quaternion.Euler(initialLookRotation);

        orientation.rotation = Quaternion.Euler(initialLookRotation.x, initialLookRotation.y, 0f);

        xRotation = initialLookRotation.x;
        yRotation = initialLookRotation.y;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        currentRotation = transform.rotation.eulerAngles;

        updateCameraPos();
    }

    void updateCameraPos(){
        Vector3 newPosition = playerPos.position + orientation.forward * 0.25f;
        newPosition.y += 3.7f;
        transform.position = newPosition;
    }
}
