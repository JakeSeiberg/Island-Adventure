using UnityEngine;

public class RoadScript : MonoBehaviour
{
    void Update()
    {
        MoveDown();

        // Destroy when out of view
        if (transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }

    void MoveDown()
    {
        transform.position += Vector3.down * RoadSpawner.gameSpeed * Time.deltaTime;
    }
}