using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using System.Drawing;

public class BikeController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    
    public float minX = -5.6f;
    
    public float maxX = 5.6f;

    private bool isInverted = false;

    private Timer time;
    private bool timeLost;
    private Vector3 playerPosition;
    private TextController pointsText;

    private bool spinning = false;


    void Start(){
        time = FindFirstObjectByType<Timer>();
        timeLost = false;
        playerPosition = new Vector3(0,0,0);

        pointsText = FindFirstObjectByType<TextController>();

    }

    void Update()
    {
        float mouseXNormalized = Input.mousePosition.x / Screen.width;

        if(isInverted)
        {
            mouseXNormalized = 1 - mouseXNormalized;
        }

        float targetX = Mathf.Lerp(minX, maxX, mouseXNormalized);

        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        if (spinning){
            transform.Rotate(0,0, 360 * Time.deltaTime);
        }

        Vector3 pos = transform.position;
        pos.y = -3.79f;
        transform.position = pos;

    }

    public void ActivateControlInversion(float duration)
    {
        // Stop any currently running inversion coroutine to reset the timer.
        StopAllCoroutines();
        StartCoroutine(InvertControlsCoroutine(duration));
    }
    
    private IEnumerator InvertControlsCoroutine(float duration)
    {
        isInverted = true;
        yield return new WaitForSeconds(duration);
        isInverted = false;
    }
    void OnCollisionEnter2D(Collision2D other){
        if (!timeLost){
            StopAllCoroutines();
            spinning = false;
            StartCoroutine(IFrameCoroutine());
        }
        
    }
    private IEnumerator IFrameCoroutine()
    {
        time.loseTime(10); 
        pointsText.points("-10");
        timeLost = true;

        yield return StartCoroutine(SpinCoroutine());

        timeLost = false;
    }

    private IEnumerator SpinCoroutine()
    {
        spinning = true;
        float rotationAmount = 0f;
        float rotationSpeed = 360f; // degrees per second

        while (rotationAmount < 180f - Mathf.Epsilon)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationStep);
            rotationAmount += rotationStep;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
        spinning = false;
    }
}
