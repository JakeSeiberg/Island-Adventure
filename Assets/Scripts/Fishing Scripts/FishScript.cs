using UnityEngine;
using System.Collections;

public class FishScript : MonoBehaviour
{
    private Vector3 direction;
    private Vector3 targetDirection;
    private float turnSpeed = 1f;

    private float fishSpeed = 15f;
    private float fishSpeedMultiplier = 1;
    private float fishDefaultAnimationSpeed = 1f;

    private float changeDirectionTime = 2f;
    private float timer;

    private Vector3 storedDirection;
    private Quaternion storedRotation; 

    public bool fishDead = false;

    public static int fishHitCount = 0;

    private bool canHitSpeedUpArea = true;
    private Animator fishAnimation;

    public Material deadMaterial;

    private float despawnTimer = 0f;

    private int despawnAfter = 10;

    private Camera mainCamera;

    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishSpeed = Random.Range(12f, 20f);
        fishDefaultAnimationSpeed = fishSpeed / 15f;


        fishAnimation = GetComponentInChildren<Animator>();
        SpawnFish();

        mainCamera = Camera.main;
        StartCoroutine(fishDespawn());
    }

    void SpawnFish()
    {
        float leftX = -120f;
        float rightX = 120f;
        float bottomZ = -35f;
        float topZ = 60f;
        
        float topSpawnZ = 82f;
        float minXAtTop = -95f;
        float maxXAtTop = 95f;

        float minY = -10f;
        float maxY = 10f;

        float targetMinX = -23f;
        float targetMaxX = 23f;
        float targetMinZ = -12f;
        float targetMaxZ = 23f;

        float xPos, zPos;
        
        int spawnEdge = Random.Range(0, 2); // 0 = left/right, 1 = top

        if (spawnEdge == 0) 
        {
            // Spawn on left or right edge
            xPos = (Random.Range(0, 2) == 0) ? leftX : rightX;
            zPos = Random.Range(bottomZ, topZ);
        }
        else 
        {
            // Spawn on the top edge
            zPos = topSpawnZ;
            xPos = Random.Range(minXAtTop, maxXAtTop);
        }

        // Random y position
        float yPos = Random.Range(minY, maxY);

        // Set the fish's position
        transform.position = new Vector3(xPos, yPos, zPos);

        // Pick a random target point inside the box
        Vector3 targetPoint = new Vector3(
            Random.Range(targetMinX, targetMaxX),
            yPos,  
            Random.Range(targetMinZ, targetMaxZ)
        );

        transform.LookAt(targetPoint);
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        // If timer finishes, change direction
        if (timer >= changeDirectionTime)
        {
            SetNewDirection();
            timer = 0;
        }

        direction = Vector3.Lerp(direction, targetDirection, Time.deltaTime * turnSpeed).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }

        transform.position += transform.forward * (fishSpeed * fishSpeedMultiplier) * Time.deltaTime;
        
        
    }

    void SetNewDirection()
    {
        targetDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        changeDirectionTime = Random.Range(1f, 5f); // Pick a new duration before changing again
    }

    public void HitBySpear()
    {
        fishDead = true;
        //delete fish
        Renderer childRenderer = GetComponentInChildren<Renderer>();
        childRenderer.material = deadMaterial;
        fishSpeedMultiplier = 0.5f;
        fishAnimation.speed = fishDefaultAnimationSpeed / 2f;

        StartCoroutine(KillFish());

    }

    private IEnumerator KillFish()
    {

        yield return new WaitForSeconds(1f);

        fishDead = true;
        playerData.fishCount++;
        playerData.sleepScore += 20;
        Destroy(gameObject);
    }

    //Speed up fish method
    public void FishAreaHit()
    {
        if (!fishDead && canHitSpeedUpArea)
        {
            //Debug.Log("Fish area hit! ");
            canHitSpeedUpArea = false;

            StartCoroutine(FishSpeedUp());
        }
    }

    private IEnumerator FishSpeedUp()
    {
        //Debug.Log("FISH SPEED UP");

        float duration = .3f; // Duration of the ramp-up
        float elapsedTime = 0f;

        float initialSpeedMultiplier = 1f;
        float targetSpeedMultiplier = 5f;

        float initialAnimationSpeed = fishDefaultAnimationSpeed;
        float targetAnimationSpeed = fishDefaultAnimationSpeed * 4f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Lerp the speed multiplier and animation speed
            fishSpeedMultiplier = Mathf.Lerp(initialSpeedMultiplier, targetSpeedMultiplier, elapsedTime / duration);
            fishAnimation.speed = Mathf.Lerp(initialAnimationSpeed, targetAnimationSpeed, elapsedTime / duration);
            yield return null; // Wait for the next frame
        }

        yield return new WaitForSeconds(3f);

        duration = 1f;
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Lerp the speed multiplier and animation speed
            fishSpeedMultiplier = Mathf.Lerp(targetSpeedMultiplier, initialSpeedMultiplier, elapsedTime / duration);
            fishAnimation.speed = Mathf.Lerp(targetAnimationSpeed, initialAnimationSpeed, elapsedTime / duration);
            yield return null; // Wait for the next frame
        }

        fishSpeedMultiplier = initialSpeedMultiplier;
        fishAnimation.speed = initialAnimationSpeed;
        canHitSpeedUpArea = true;

        //Debug.Log("Fish speed reset to normal.");
    }

    private IEnumerator fishDespawn()
    {
        while(true)
        {
            Renderer fishRenderer = GetComponentInChildren<Renderer>();
            if (fishRenderer != null)
            {
                Vector3 viewportPoint = mainCamera.WorldToViewportPoint(fishRenderer.bounds.center);

                if (viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                    viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
                    viewportPoint.z > 0)
                {
                    despawnTimer = 0;
                }
                else
                {
                    despawnTimer += 1;
                }
            }
            if (despawnTimer >= despawnAfter)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(1f);
            //debug log fish identifier and despawn timer
            //Debug.Log("Fish ID: " + gameObject.GetInstanceID() + " despawn timer: " + despawnTimer);
        }
    }

}