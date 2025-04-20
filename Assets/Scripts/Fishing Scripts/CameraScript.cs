using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Vector3 topViewPos = new Vector3(0f, 67.3f, -29.1f);
    Vector3 topViewRot = new Vector3(66.8f, 0f, 0f);

    Vector3 sideViewPos = new Vector3(382.4f, 0f, 0f);
    Vector3 sideViewRot = new Vector3(0f, -90f, 0f);

    private Camera cam;


    void Start()
    {
        cam = GetComponent<Camera>(); 
        cam.orthographic = false;

        transform.position = topViewPos;

        transform.rotation = Quaternion.Euler(topViewRot);
    }


}