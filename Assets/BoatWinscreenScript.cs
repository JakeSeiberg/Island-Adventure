using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatWinscreenScript : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(86.97f, 1.553683f, 22.77695f);
    private Vector3 endPosition = new Vector3(86.97f, 1.553683f, -165f); // You originally wanted way further out
    private float totalTravelTime = 32f; // Total journey time
    private float fadeStartTime = 20f;   // When fade starts
    private float fadeDuration = 10f;    // Fade time
    public CanvasGroup fadeCanvasGroup;

    private float timer = 0f;

    void Start()
    {
        GameObject destroy = GameObject.FindGameObjectWithTag("DontDestroyOnLoad");
        if (destroy != null)
            Destroy(destroy);

        transform.position = startPosition;
        if (fadeCanvasGroup != null)
            fadeCanvasGroup.alpha = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Move boat
        float t = Mathf.Clamp01(timer / totalTravelTime);
        transform.position = Vector3.Lerp(startPosition, endPosition, t);

        // Handle fade
        if (timer >= fadeStartTime && timer <= fadeStartTime + fadeDuration)
        {
            float fadeT = (timer - fadeStartTime) / fadeDuration;
            if (fadeCanvasGroup != null)
                fadeCanvasGroup.alpha = Mathf.Clamp01(fadeT);
        }
        else if (timer > fadeStartTime + fadeDuration)
        {
            if (fadeCanvasGroup != null)
                fadeCanvasGroup.alpha = 1f; // Fully black
        }

        // End game
        if (timer >= 32f)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerData.curScene = "StartMenu";
        SceneManager.LoadScene("StartMenu");
    }
}
