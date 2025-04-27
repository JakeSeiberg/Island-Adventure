using UnityEngine;

public class BeerSpawner : MonoBehaviour
{
    [Tooltip("A prefab that is instantiated when the asteroid is destroyed")]
    public float spawnWidth = 1;
    [Tooltip("How many obstacles spawn per second?")]
    public float spawnRate = 1;
    [Tooltip("The prefab that is to be instantiated as obstacles")]
    public GameObject beerPrefab;
    private float lastSpawnTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSpawnTime + 1 / spawnRate < Time.time) {
            lastSpawnTime = Time.time;
            Vector3 spawnPosition = transform.position;
            spawnPosition += new Vector3(Random.Range(-spawnWidth, spawnWidth), 0, 0);
			// the Instatiate function creates a new GameObject copy (clone) from a Prefab at a specific location and orientation.
            Instantiate(beerPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void OnDrawGizmos(){
		Gizmos.DrawLine (transform.position - new Vector3 (spawnWidth, 0, 0), transform.position + new Vector3 (spawnWidth, 0, 0));
		Gizmos.DrawLine (transform.position - new Vector3 (spawnWidth, 1, 0), transform.position - new Vector3 (spawnWidth, -1, 0));
		Gizmos.DrawLine (transform.position + new Vector3 (spawnWidth, 1, 0), transform.position + new Vector3 (spawnWidth, -1, 0));
	}
}

