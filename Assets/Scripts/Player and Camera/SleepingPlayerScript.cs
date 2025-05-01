using UnityEngine;
using System.Collections;

public class SleepingPlayerScript : MonoBehaviour
{
    private float sensX = 100f;
    private float sensY = 100f;

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

    void Start()
    {
        playerData.curScene = "SleepScene";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 initialLookRotation = new Vector3(0f, 90f, 0f);

        transform.rotation = Quaternion.Euler(initialLookRotation);

        xRotation = initialLookRotation.x;
        yRotation = initialLookRotation.y;
        initialYRotation = yRotation;

        blackOut.alpha = 0f;
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
<<<<<<< HEAD
        float fadeDuration = 2f; 
=======
        float fadeDuration = 2f;
>>>>>>> a2ac5c6552a9075b436a0f256def509f0a55c75e
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            blackOut.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        blackOut.alpha = 1f;
<<<<<<< HEAD
        yield return new WaitForSeconds(2f); 
=======
        yield return new WaitForSeconds(2f);
>>>>>>> a2ac5c6552a9075b436a0f256def509f0a55c75e
        playerData.sleepScore = 0;
        playerData.canSleep = false;
        SceneSwitcher.changeScene();
    }
}
