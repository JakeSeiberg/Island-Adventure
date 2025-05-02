using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour
{

    public GameObject player; // Reference to the player GameObject
    public static int level;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            TeleportPlayerToStartPosition(2.62f, -9.16f);
            level = 0;
        }
        else if (SceneManager.GetActiveScene().name == "LevelOne")
        {
            TeleportPlayerToStartPosition(42.6f, -3.11f);
            level = 1;
        }
        else if (SceneManager.GetActiveScene().name == "LevelTwo")
        {
            TeleportPlayerToStartPosition(71.3f, -3.11f);
            level = 2;
        }
        else if (SceneManager.GetActiveScene().name == "LevelThree")
        {
            TeleportPlayerToStartPosition(112.3f,-4.22f);
            level = 3;
        }

    }

    void TeleportPlayerToStartPosition(float x, float y)
    {
        player.transform.position = new Vector3(x, y, player.transform.position.z);
    }
}
