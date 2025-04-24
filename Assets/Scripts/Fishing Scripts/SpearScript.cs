using UnityEngine;
using System.Collections;

public class SpearUIScript : MonoBehaviour
{
    private Vector3 spearPosDefault = new Vector3(1.64f, 67.5f, -30.62f);
    private Vector3 spearRotDefault = new Vector3(61.7f, 50.3f, 120.9f);

    private Vector3 spearPosPullback = new Vector3(0.2f, 70.85f, -32.31f);
    private Vector3 spearRotPullback = new Vector3(61.7f, 50.3f, 120.9f);

    private bool isHoldingMouse = false; // Tracks if the mouse is being held down
    private bool canShoot = true;
    private bool isShooting = false;
    private float moveSpeed = 2.5f;
    private bool forceShoot = false;

    private float minXRotation = 73.2f;
    private float maxXRotation = 49f;

    private float holdTime = 0f; // Tracks how long the mouse button is held
    private float maxHoldTime = 1.6062f; // Maximum time to reach full speed
    private float minSpeed = .1f; // Minimum throw speed
    private float maxSpeed = 100f;

    private Rigidbody rb; // Reference to the Rigidbody

    void Start()
    {
        // Initialize the Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing!");
        }

        // Reset spear position and rotation
        resetSpear();
    }

    void Update()
    {

        // Only update the spear's rotation if it's not shooting
        if (!isShooting)
        {
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                isHoldingMouse = true;
                holdTime = 0f;
            }

            if (isHoldingMouse)
            {
                holdTime += Time.deltaTime;
            }

            // Detect when the mouse button is released
            if ((Input.GetMouseButtonUp(0) && isHoldingMouse) || forceShoot)
            {
                isHoldingMouse = false;
                if (holdTime >= 1.28f)
                {
                    playerData.hasThrownStrongSpear = true; 
                }
                ShootSpear(); // Start the shooting process
                forceShoot = false;
            }

            if (isHoldingMouse)
            {
                MoveSpearToPullback();
            }

            UpdateSpearRotation(); // Update spear rotation based on mouse position
        }

        if (transform.position.y < -60f) // Adjust the Y threshold as needed
        {
            resetSpear();
        }
    }

    void FixedUpdate()
    {
        // Ensure the spear's rotation matches its velocity direction
        if (rb != null && rb.linearVelocity != Vector3.zero && isShooting)
        {
            // Rotate the spear to face its velocity direction
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity.normalized);
        }
    }

    private void UpdateSpearRotation()
    {
        // Get the mouse's X position in screen space
        float mouseX = Input.mousePosition.x;

        // Map the mouse's X position to the rotation range
        float screenWidth = Screen.width;
        float xRotation = Mathf.Lerp(minXRotation, maxXRotation, mouseX / screenWidth);

        // Apply the new rotation to the spear
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(xRotation, currentRotation.y, currentRotation.z);
    }

    private void MoveSpearToPullback()
    {
        // Move the spear toward spearPosPullback
        transform.position = Vector3.MoveTowards(transform.position, spearPosPullback, moveSpeed * Time.deltaTime);

        // Stop moving if the spear reaches spearPosPullback
        if (Vector3.Distance(transform.position, spearPosPullback) < 0.01f)
        {
            isHoldingMouse = false; // Stop movement
            forceShoot = true;      // Set forceShoot to true
        }
    }

    private void ShootSpear()
    {
        // Perform a raycast from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPoint;

        // Check if the ray hits something
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; // Use the hit point as the target
        }
        else
        {
            // If the ray doesn't hit anything, shoot in the direction the camera is facing
            targetPoint = ray.GetPoint(100f); // A point 100 units away from the camera
        }

        // Calculate the direction to the target point
        Vector3 shootDirection = (targetPoint - transform.position).normalized;

        // Rotate the spear to face the target direction
        transform.rotation = Quaternion.LookRotation(shootDirection);

        // Apply an initial velocity to the Rigidbody
//        Debug.Log("Hold time: " + holdTime);
        float throwSpeed = Mathf.Lerp(minSpeed, maxSpeed, holdTime / maxHoldTime);

        rb.linearVelocity = shootDirection * throwSpeed;

        // Enable gravity
        rb.useGravity = true;

        isShooting = true; // Set shooting state to true
        holdTime = 0f;

        AudioManager.Instance.playGrunt();
        AudioManager.Instance.playSplash();
    }

    private void resetSpear()
    {
        // Reset the spear's position and rotation
        transform.position = spearPosDefault;
        transform.rotation = Quaternion.Euler(spearRotDefault);

        // Reset the Rigidbody's velocity and disable gravity
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.useGravity = false;
        }

        // Reset the cooldown
        canShoot = true;

        // Reset the mouse holding state
        isHoldingMouse = false;

        // Reset the force shoot state
        forceShoot = false;

        // Reset the shooting state
        isShooting = false;

        holdTime = 0f;

//        Debug.Log("Spear reset to default position and rotation.");
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Platform"))
        {
            rb.linearVelocity = Vector3.zero; // Stop the Rigidbody's velocity
            rb.useGravity = false;      // Disable gravity

            Collider spearCollider = GetComponent<Collider>(); // Get the spear's Collider component
            Vector3 colliderPosition = spearCollider.bounds.center; // Get the center of the collider in world space
//            Debug.Log("Spear Collider Position: " + colliderPosition);
            
            StartCoroutine(ResetSpearAfterDelay(1f));
        }
        else
        {
            FishScript fishScript = other.transform.parent?.GetComponent<FishScript>();
            if (other.CompareTag("FishTarget"))
            {
                fishScript.HitBySpear(); // Call the HitBySpear method on the FishScript
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FishAreaCollider"))
        {
            FishScript fishScript = other.transform.parent?.GetComponent<FishScript>();
            fishScript.FishAreaHit();
            Debug.Log("Fish area hit!");
        }
    }

    private IEnumerator ResetSpearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        resetSpear(); // Call the resetSpear method
    }

}