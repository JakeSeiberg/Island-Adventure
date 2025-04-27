using UnityEngine;

public class CigsSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("Half-width (in world units) of the spawn area along the x-axis.")]
    public float spawnWidth = 1f;
    
    [Tooltip("How many pickups spawn per second?")]
    public float spawnRate = 1.5f;
    
    [Tooltip("The cigarette pickup prefab.")]
    public GameObject cigsPrefab;
    
    private float lastSpawnTime = 0f;

    void Update()
    {
        // Check if it is time to spawn a new pickup based on spawnRate.
        if (lastSpawnTime + 1f / spawnRate < Time.time)
        {
            lastSpawnTime = Time.time;
            
            // Determine a random x offset within the specified spawnWidth.
            Vector3 spawnPosition = transform.position;
            spawnPosition += new Vector3(Random.Range(-spawnWidth, spawnWidth), 0, 0);
            
            // Instantiate the cigarette pickup.
            Instantiate(cigsPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void OnDrawGizmos(){
		Gizmos.DrawLine (transform.position - new Vector3 (spawnWidth, 0, 0), transform.position + new Vector3 (spawnWidth, 0, 0));
		Gizmos.DrawLine (transform.position - new Vector3 (spawnWidth, 1, 0), transform.position - new Vector3 (spawnWidth, -1, 0));
		Gizmos.DrawLine (transform.position + new Vector3 (spawnWidth, 1, 0), transform.position + new Vector3 (spawnWidth, -1, 0));
	}

}
