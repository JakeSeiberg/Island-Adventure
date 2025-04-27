using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Tooltip("A prefab that is instantiated when the asteroid is destroyed")]
    public float spawnWidth = 1;
    [Tooltip("How many obstacles spawn per second?")]
    public float spawnRate = 1;
    public float minSpawnrate = 0.2f;
    public float maxSpawnrate = 1;
    [Tooltip("The prefab that is to be instantiated as obstacles")]
    public GameObject obstaclePrefab;
    private float lastSpawnTime = 0;
    private float trueSpawnRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trueSpawnRate = spawnRate * Random.Range(minSpawnrate, maxSpawnrate);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSpawnTime + 1 / trueSpawnRate < Time.time) {
            lastSpawnTime = Time.time;
            Vector3 spawnPosition = transform.position;
            spawnPosition += new Vector3(Random.Range(-spawnWidth, spawnWidth), 0, 0);
			// the Instatiate function creates a new GameObject copy (clone) from a Prefab at a specific location and orientation.
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            trueSpawnRate = spawnRate * Random.Range(minSpawnrate, maxSpawnrate);
        }
    }

    void OnDrawGizmos(){
		Gizmos.DrawLine (transform.position - new Vector3 (spawnWidth, 0, 0), transform.position + new Vector3 (spawnWidth, 0, 0));
		Gizmos.DrawLine (transform.position - new Vector3 (spawnWidth, 1, 0), transform.position - new Vector3 (spawnWidth, -1, 0));
		Gizmos.DrawLine (transform.position + new Vector3 (spawnWidth, 1, 0), transform.position + new Vector3 (spawnWidth, -1, 0));
	}
}
