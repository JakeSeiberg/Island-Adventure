using UnityEngine;
using System.Collections;

public class WormSpawner : MonoBehaviour
{
    public GameObject wormPrefab; // Assign the worm prefab in the Inspector
    public LayerMask groundLayer;

    void Start()
    {
        StartCoroutine(SpawnWorms());
        StartCoroutine(tip());
    }

    private IEnumerator SpawnWorms()
    {
        for (int i = 0; i < 200; i++)
        {
            float randomX = Random.Range(40f, 160f);
            float randomZ = Random.Range(45f, 99f);
            float raycastHeight = 100f;
            Vector3 rayOrigin = new Vector3(randomX, raycastHeight, randomZ);

            Ray ray = new Ray(rayOrigin, Vector3.down);
            RaycastHit hit;

            // Only hit the groundLayer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                Vector3 spawnPosition = hit.point + Vector3.up * 0.05f;
                Instantiate(wormPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(0.001f);
        }
    }

    private IEnumerator tip()
    {
        yield return new WaitForSeconds(15f);
        while (!playerData.hasPickedUpAWorm)
        {
            if (playerData.curScene == "MainWorld")
            {
                toolTips.tip("Use E to collect worms! They might be good bait for fishing", 5f);
            }

            yield return new WaitForSeconds(20f); // Waits regardless of scene, avoids tight loop
        }
    }
}