using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    [Tooltip("How fast does the obstacle move in units per second")]
	public float speed = 10;
    [Tooltip("How long does the obstacle life before it is automatically destroyed, in seconds")]
    public float lifeTime = 10;
    Vector2 direction = new Vector2();
    public AudioClip honk;
    private AudioSource source;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = new Vector2(0,-1);
        direction.Normalize();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(direction.x, direction.y, 0) * (speed + RoadSpawner.gameSpeed) * Time.deltaTime;
		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0){
			Destroy(gameObject);
		}
    }

    void OnCollisionEnter2D(){
        source.PlayOneShot(honk);
    }
}
