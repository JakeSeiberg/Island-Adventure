using UnityEngine;
using UnityEngine.SceneManagement; 

public class _PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            _AudioManager.Instance.stopSfx();
            _AudioManager.Instance.playDeathSound();
            RestartScene();
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
