using UnityEngine;
using System.Collections;

public class PhysicalWormScript : MonoBehaviour
{
    Vector3 startPos = new Vector3(.1f, 64.3f, -30.36f);
    Vector3 startRot = new Vector3(0f, 205.6f, 0f);

    private float minAngle = -20f; 
    private float maxAngle = 20f;

    private MeshRenderer wormRenderer;

    void Start()
    {
        wormRenderer = GetComponent<MeshRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(float minDegree, float maxDegree)
    {
        minAngle = minDegree;
        maxAngle = maxDegree;

        throwWorm();
    }

    void throwWorm()
    {
        transform.position = startPos;

        transform.rotation = Quaternion.Euler(startRot.x, Random.Range(0f, 360f), Random.Range(0f, 360f));

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached to the worm!");
            return;
        }

        float randomAngle = Random.Range(minAngle, maxAngle);

        Vector3 throwDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;

        rb.linearVelocity = throwDirection * 10f; // Adjust speed as needed

        rb.angularVelocity = new Vector3(
            Random.Range(-1f, 1f), // Random x-axis rotation velocity
            Random.Range(-1f, 1f), // Random y-axis rotation velocity
            Random.Range(-1f, 1f)  // Random z-axis rotation velocity
        );
    }

    void FixedUpdate()
    {
        if (transform.position.y < -60f) // Adjust the Y threshold as needed
        {
            Destroy(gameObject);
        }

        AdjustFallRate();
        UpdateOpacityBasedOnY();
    }

    private void AdjustFallRate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached to the worm!");
            return;
        }

        if (transform.position.y < 35f)
        {
            rb.linearDamping = 3f; // Adjust this value to control the slowing effect
        }
        else
        {
            rb.linearDamping = 0f;
        }
    }
    
    private void UpdateOpacityBasedOnY()
    {

        float yPos = transform.position.y;

        if (yPos <= 28f && yPos >= -11f)
        {
            float opacity = 1 - (Mathf.InverseLerp(28f, -11f, yPos)); 
            Color color = wormRenderer.material.color;
            color.a = opacity;
            wormRenderer.material.color = color;
        }
        else if (yPos < -11f)
        {
            Color color = wormRenderer.material.color;
            color.a = 0f;
            wormRenderer.material.color = color;
        }
    }
}
