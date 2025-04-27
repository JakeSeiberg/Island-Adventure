using Unity.VisualScripting;
using UnityEngine;

public class TrashPickupVariant : MonoBehaviour
{
    private float speed = RoadSpawner.gameSpeed;

    [Tooltip("How long does the obstacle life before it is automatically destroyed, in seconds")]
    public float lifeTime = 10;
    Vector2 direction = new Vector2();

    private Timer time;
    private Vector3 goToPos;
    private bool reachedPos = false;

    void Start()
    {
        direction = new Vector2(0,-1);
        direction.Normalize();
        goToPos = new Vector3
        (transform.position.x - Random.Range(-1f , 1) , 
        transform.position.y + Random.Range(.1f, 2), 
        transform.position.z);

        time = FindFirstObjectByType<Timer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == goToPos)
        {
            reachedPos = true;
        }
        {
            transform.position = Vector3.MoveTowards(transform.position, goToPos, 3f * Time.deltaTime);
        }

        if (reachedPos){
            transform.position = transform.position + new Vector3(direction.x, direction.y, 0) * (speed * Time.deltaTime *1.5f);
		    lifeTime -= Time.deltaTime;
        }
    }
}
