using UnityEngine;
using System.Collections;

public class SpearUIScript : MonoBehaviour
{
    private Vector3 spearPosDefault = new Vector3(1.64f, 67.5f, -30.62f);
    private Vector3 spearRotDefault = new Vector3(61.7f, 50.3f, 120.9f);

    private Vector3 spearPosPullback = new Vector3(0.2f, 70.85f, -32.31f);
    private Vector3 spearRotPullback = new Vector3(61.7f, 50.3f, 120.9f);

    private bool isHoldingMouse = false; 
    private bool canShoot = true;
    private bool isShooting = false;
    private float moveSpeed = 2.5f;
    private bool forceShoot = false;

    private float minXRotation = 73.2f;
    private float maxXRotation = 49f;

    private float holdTime = 0f; 
    private float maxHoldTime = 1.6062f; 
    private float minSpeed = .1f; 
    private float maxSpeed = 100f;

    private Rigidbody rb; 

    public AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing!");
        }

        resetSpear();
    }

    void Update()
    {
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

            if ((Input.GetMouseButtonUp(0) && isHoldingMouse) || forceShoot)
            {
                isHoldingMouse = false;
                if (holdTime >= 1.28f)
                {
                    playerData.hasThrownStrongSpear = true; 
                }
                shootSpear(); 
                forceShoot = false;
            }

            if (isHoldingMouse)
            {
                moveSpearToPullback();
            }

            updateSpearRotation(); 
        }

        if (transform.position.y < -60f) 
        {
            resetSpear();
        }
    }

    void FixedUpdate()
    {
        if (rb != null && rb.linearVelocity != Vector3.zero && isShooting)
        {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity.normalized);
        }
    }

    private void updateSpearRotation()
    {
        float mouseX = Input.mousePosition.x;

        float screenWidth = Screen.width;
        float xRotation = Mathf.Lerp(minXRotation, maxXRotation, mouseX / screenWidth);

        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(xRotation, currentRotation.y, currentRotation.z);
    }

    private void moveSpearToPullback()
    {
        transform.position = Vector3.MoveTowards(transform.position, spearPosPullback, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, spearPosPullback) < 0.01f)
        {
            isHoldingMouse = false; 
            forceShoot = true;     
        }
    }

    private void shootSpear()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; 
        }
        else
        {
            targetPoint = ray.GetPoint(100f); 
        }

        Vector3 shootDirection = (targetPoint - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(shootDirection);

        float throwSpeed = Mathf.Lerp(minSpeed, maxSpeed, holdTime / maxHoldTime);

        rb.linearVelocity = shootDirection * throwSpeed;

        rb.useGravity = true;

        isShooting = true; 
        holdTime = 0f;

        audioManager.playGrunt();
        audioManager.playSplash();
    }

    private void resetSpear()
    {
        transform.position = spearPosDefault;
        transform.rotation = Quaternion.Euler(spearRotDefault);

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.useGravity = false;
        }

        canShoot = true;

        isHoldingMouse = false;

        forceShoot = false;

        isShooting = false;

        holdTime = 0f;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Platform"))
        {
            rb.linearVelocity = Vector3.zero; 
            rb.useGravity = false;      

            Collider spearCollider = GetComponent<Collider>(); 
            Vector3 colliderPosition = spearCollider.bounds.center; 
            
            StartCoroutine(resetSpearAfterDelay(1f));
        }
        else
        {
            FishScript fishScript = other.transform.parent?.GetComponent<FishScript>();
            if (other.CompareTag("FishTarget"))
            {
                fishScript.HitBySpear(); 
                audioManager.playFishHit();
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

    private IEnumerator resetSpearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        resetSpear(); 
    }

}