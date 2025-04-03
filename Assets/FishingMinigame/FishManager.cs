using UnityEngine;
using System.Collections;

public class FishManager : MonoBehaviour
{
    public GameObject fishPrefab; 
    private int fishCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       StartCoroutine(SpawnFish());
       StartCoroutine(UpdateFishCount());
    }

    private IEnumerator UpdateFishCount()
    {
        while (true)
        {
            fishCount = CountFishInArea(); 
            yield return new WaitForSeconds(1f);
        }
    }

    public void restartFishSpawning()
    {
        StopCoroutine(SpawnFish());
        StartCoroutine(SpawnFish());
        //Debug.Log("Fish spawning restarted");
    }

    private IEnumerator SpawnFish()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f / WormManager.wormMultiplier, 25f / WormManager.wormMultiplier));
            if (fishCount <= (3 * WormManager.wormSpawnMultiplier))
            {
                
                Instantiate(fishPrefab);
                
            }
            
        }
    }

    private int CountFishInArea()
    {
        GameObject[] fishObjects = GameObject.FindGameObjectsWithTag("Fish");
        int count = 0;

        Camera mainCamera = Camera.main;

        foreach (GameObject fish in fishObjects)
        {
            Renderer fishRenderer = fish.GetComponentInChildren<Renderer>();

            if (fishRenderer != null)
            {
                Vector3 viewportPoint = mainCamera.WorldToViewportPoint(fishRenderer.bounds.center);

                if (viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                    viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
                    viewportPoint.z > 0)
                {
                    count++;
                }
            }
        }

        return count;
    }
}