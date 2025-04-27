using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    private float speed = RoadSpawner.gameSpeed;

    [Tooltip("How long does the obstacle life before it is automatically destroyed, in seconds")]
    public float lifeTime = 10;
    Vector2 direction = new Vector2();

    public GameObject[] trashVariants;

    private Timer time;

    private TextController pointsText;
    public AudioClip trash;
    private AudioSource source;


    void Start()
    {
        direction = new Vector2(0,-1);
        direction.Normalize();

        time = FindFirstObjectByType<Timer>();

        pointsText = FindFirstObjectByType<TextController>();
        source = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(direction.x, direction.y, 0) * RoadSpawner.gameSpeed * Time.deltaTime;
		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0){
			Destroy(gameObject);
		}
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has a BikeController component.
        BikeController bike = other.GetComponent<BikeController>();
        if (bike != null)
        {
            // Invert the bike's controls for the specified duration.
            time.loseTime(5);
            pointsText.points("-5");

            
            // Optionally, add sound or visual effects here.
            
            Instantiate(trashVariants[0], transform.position, transform.rotation);
            Instantiate(trashVariants[1], transform.position, transform.rotation);
            Instantiate(trashVariants[2], transform.position, transform.rotation);
            Instantiate(trashVariants[3], transform.position, transform.rotation);

            // Destroy the trash pickup after the collision.
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(){
        source.PlayOneShot(trash);
    }
}
