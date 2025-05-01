using UnityEngine;
using System.Collections;

public class LeafFallingScript : MonoBehaviour
{
    private Rigidbody rb;

    private float swaySpeed = 2f;
    private float swayAmount = 1.2f;
    private float fallSpeed = 2f;

    private Vector3 spinAxis;
    private float spinSpeed;
    private bool hasLanded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        spinAxis = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        spinSpeed = Random.Range(20f, 60f); 
    }

    void FixedUpdate()
    {
        if (hasLanded){
            rb.linearVelocity = new Vector3(
            Mathf.Sin(Time.time * swaySpeed) * swayAmount, 
            -fallSpeed, 
            Mathf.Cos(Time.time * swaySpeed) * swayAmount 
        );
        }
        else{

            rb.linearVelocity = new Vector3(
                Mathf.Sin(Time.time * swaySpeed) * swayAmount,
                -fallSpeed,
                Mathf.Cos(Time.time * swaySpeed) * swayAmount
            );

            transform.Rotate(spinAxis, spinSpeed * Time.fixedDeltaTime, Space.Self);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded && collision.gameObject.GetComponent<Terrain>() != null)
        {
            StartCoroutine(DisableKinematicsAfterDelay(3f));
        }
    }

    private IEnumerator DisableKinematicsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        hasLanded = true;
        rb.isKinematic = false;
    }

    public void leafCollected()
    {
        playerData.leafCount++;
        Destroy(this.gameObject);
    }
}