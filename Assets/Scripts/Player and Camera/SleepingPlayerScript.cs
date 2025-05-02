using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SleepingPlayerScript : MonoBehaviour
{
    private float sensX = 150f;
    private float sensY = 150f;

    public Transform playerPos;

    private float verticalLookLimitMin = -100f;
    private float verticalLookLimitMax = 30f; 
    private float horizontalLookLimit = 60f;   

    float xRotation;
    float yRotation;
    float initialYRotation;

    private static Vector3 currentRotation;

    public CanvasGroup blackOut;
    
    public SceneSwitcher SceneSwitcher;

    private bool playGame = false;

    void Start()
    {
        playerData.curScene = "SleepScene";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 initialLookRotation = new Vector3(0f, 220f, 0f);

        transform.rotation = Quaternion.Euler(initialLookRotation);

        xRotation = initialLookRotation.x;
        yRotation = initialLookRotation.y;
        initialYRotation = yRotation;

        blackOut.alpha = 0f;
        if (!playerData.hasPlayedHALO)
        {
            playGame = true;
        }
        else
        {
            playGame = Random.Range(0f, 1f) < 0.4f;
        }
        StartCoroutine(fadeInBlackOut());
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, verticalLookLimitMin, verticalLookLimitMax);

        float horizontalOffset = Mathf.DeltaAngle(initialYRotation, yRotation);
        horizontalOffset = Mathf.Clamp(horizontalOffset, -horizontalLookLimit, horizontalLookLimit);
        yRotation = initialYRotation + horizontalOffset;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        currentRotation = transform.rotation.eulerAngles;

    }

    private IEnumerator fadeInBlackOut()
    {
        yield return new WaitForSeconds(2f);
        if (playGame)
        {
            toolTips.tip("You start to fall into a deep sleep...",4f);
        }

        float fadeDuration = 4f; 
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            blackOut.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        blackOut.alpha = 1f;
        yield return new WaitForSeconds(2f);
        playerData.sleepScore = 0;
        playerData.canSleep = false;

        if (playGame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            toolTips.changeScene();
            playerData.curScene = "HALO";
            playerData.hasPlayedHALO = true;
            playerData.sleepTooltipToggle = false;
            SceneManager.LoadScene("Main Menu"); 
        }
        else
        {
            SceneSwitcher.changeScene();
        }
        
    }
}
