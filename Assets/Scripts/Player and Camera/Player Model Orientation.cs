using UnityEngine;

public class PlayerModelOrientation : MonoBehaviour
{
    public Transform Orientation;

    void Update()
    {
        Vector3 tmpPos = Orientation.position;
        tmpPos.y += 2;
        transform.position = tmpPos;

        transform.rotation = Orientation.rotation;
    }
}
