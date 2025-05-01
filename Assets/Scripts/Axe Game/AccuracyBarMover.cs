using UnityEngine;

public class AccuracyBarMover : MonoBehaviour
{
    public float speed = 4f;                 
    public float upperLimit = 4.34f;         
    public float lowerLimit = -4.34f;        

    private bool movingUp = true;

    void Update()
    {
        float direction = movingUp ? 1f : -1f;

        transform.Translate(Vector3.up * direction * speed * Time.deltaTime);

        if (transform.position.y >= upperLimit)
        {
            movingUp = false;
        }
        else if (transform.position.y <= lowerLimit)
        {
            movingUp = true;
        }
    }
}
