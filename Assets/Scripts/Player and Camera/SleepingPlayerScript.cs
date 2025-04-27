using UnityEngine;
using System.Collections;

public class SleepingPlayerScript : MonoBehaviour
{
    private float sensX = 100f;
    private float sensY = 100f;

    public Transform playerPos;

    private float verticalLookLimitMin = -100f; // Look "down" toward feet
    private float verticalLookLimitMax = 30f;  // Look "up" toward ceiling
    private float horizontalLookLimit = 60f;   // 60° left/right from initial forward

    float xRotation;
    float yRotation;
    float initialYRotation;

    private static Vector3 currentRotation;

    public CanvasGroup blackOut;
    
    public sceneSwitcher sceneSwitcher;

    void Start()
    {
        playerData.curScene = "SleepScene";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set the initial rotation to look 90 degrees to the right
        Vector3 initialLookRotation = new Vector3(0f, 90f, 0f);

        transform.rotation = Quaternion.Euler(initialLookRotation);

        // Initialize rotation variables
        xRotation = initialLookRotation.x;
        yRotation = initialLookRotation.y;
        initialYRotation = yRotation;

        // Set CanvasGroup alpha to 0
        blackOut.alpha = 0f;
        StartCoroutine(FadeInBlackOut());
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        // Clamp vertical angle (can't look fully down)
        xRotation = Mathf.Clamp(xRotation, verticalLookLimitMin, verticalLookLimitMax);

        // Clamp horizontal angle relative to initial direction
        float horizontalOffset = Mathf.DeltaAngle(initialYRotation, yRotation);
        horizontalOffset = Mathf.Clamp(horizontalOffset, -horizontalLookLimit, horizontalLookLimit);
        yRotation = initialYRotation + horizontalOffset;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        currentRotation = transform.rotation.eulerAngles;

    }

    private IEnumerator FadeInBlackOut()
    {
        yield return new WaitForSeconds(2f);
        float fadeDuration = 2f; // Duration of the fade in seconds
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            blackOut.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        blackOut.alpha = 1f; // Ensure it's fully opaque at the end
        yield return new WaitForSeconds(2f); // Wait for 1 second before fading out
        playerData.sleepScore = 0;
        playerData.canSleep = false;
        sceneSwitcher.changeScene();
    }
}
