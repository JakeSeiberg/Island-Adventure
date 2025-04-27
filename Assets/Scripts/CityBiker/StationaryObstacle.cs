using UnityEngine;

public class StationaryObstacle : MonoBehaviour
{
    [Tooltip("How long does the obstacle life before it is automatically destroyed, in seconds")]
    public float lifeTime = 10;
    Vector2 direction = new Vector2();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = new Vector2(0,-1);
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * RoadSpawner.gameSpeed * Time.deltaTime;
		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0){
			Destroy(gameObject);
		}
    }
}
