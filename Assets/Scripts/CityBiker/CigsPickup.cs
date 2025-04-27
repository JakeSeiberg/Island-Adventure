using UnityEngine;

public class CigsPickup : MonoBehaviour
{
    
    [Tooltip("How long does the pickup live before being automatically destroyed (in seconds)?")]
    public float lifeTime = 10f;
    
    // The downward direction (normalized)
    private Vector2 direction = new Vector2(0, -1);
    private Timer time;

    private TextController pointsText;
    public AudioClip smoke;
    private AudioSource source;

    void Start()
    {
        direction.Normalize();
        time = FindFirstObjectByType<Timer>();

        pointsText = FindFirstObjectByType<TextController>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Move the pickup downward
        transform.position += new Vector3(direction.x, direction.y, 0) * RoadSpawner.gameSpeed * Time.deltaTime;
        
        // Decrease lifetime and destroy if expired
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    // This uses 2D physics, so ensure your collider is a 2D collider set as a Trigger.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Option 1: Check for the BikeController component (if your bike has one)
        BikeController bike = other.GetComponent<BikeController>();
        if (bike != null)
        {
            // Find your cigs UI manager script in the scene and increment the count.
            CigsScript cigsManager = FindAnyObjectByType<CigsScript>();
            if (cigsManager != null)
            {
                CigsScript.cigsCount++;
                time.loseTime(-3); 
                pointsText.points("+3");

            }
            
            // Destroy the cigarette pickup upon collection.
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(){
        source.PlayOneShot(smoke);
    }
}
