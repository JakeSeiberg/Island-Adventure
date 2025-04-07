using UnityEngine;

public class WormSpawner : MonoBehaviour
{
    public GameObject wormPrefab; // Assign the worm prefab in the Inspector

    void Start()
    {
        SpawnWorms();
    }

    private void SpawnWorms()
    {
        for (int i = 0; i < 100; i++)
        {
            // Generate random positions within the specified bounds
            float randomX = Random.Range(40f, 160f);
            float randomZ = Random.Range(45f, 99f);
            float y = 14f;

            Vector3 spawnPosition = new Vector3(randomX, y, randomZ);

            // Instantiate the wormPrefab at the random position with no rotation
            Instantiate(wormPrefab, spawnPosition, Quaternion.identity);
        }
    }
}