using UnityEngine;
using System.Collections.Generic;

public class RoadSpawner : MonoBehaviour
{
    public GameObject RoadPrefab;
    static public float gameSpeed = 6f;
    
    private float _roadLength;
    private List<GameObject> _roads = new List<GameObject>();

    void Start()
    {
        _roadLength = RoadPrefab.GetComponent<Renderer>().bounds.size.y;
        gameSpeed = 6f;
        // Spawn initial roads to cover movement
        for (int i = -1; i <= 1; i++)
        {
            SpawnRoad(i * _roadLength);
        }
    }

    void Update()
    {
        CheckAndSpawnRoad();
    }

    void CheckAndSpawnRoad()
    {
        if (_roads.Count > 0)
        {
            GameObject lastRoad = _roads[_roads.Count - 1];

            // Spawn when the last road moves down enough
            if (lastRoad.transform.position.y <= 0)
            {
                SpawnRoad(lastRoad.transform.position.y + _roadLength);
                
                // Remove old roads
                Destroy(_roads[0]);
                _roads.RemoveAt(0);
            }
        }
    }

    void SpawnRoad(float yPos)
    {
        Vector3 spawnPosition = new Vector3(0.0f, yPos - 1, 0.0f);
        GameObject newRoad = Instantiate(RoadPrefab, spawnPosition, Quaternion.identity);
        _roads.Add(newRoad);
    }
}

