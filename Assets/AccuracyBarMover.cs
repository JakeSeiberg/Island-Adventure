using UnityEngine;

public class AccuracyBarMover : MonoBehaviour
{
    public float speed = 4f;                 
    public float upperLimit = 4.34f;         
    public float lowerLimit = -4.34f;        

    private bool movingUp = true;

    void Update()
    {
        // Determine direction
        float direction = movingUp ? 1f : -1f;

        // Move the bar
        transform.Translate(Vector3.up * direction * speed * Time.deltaTime);

        // Reverse direction when hitting bounds
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
