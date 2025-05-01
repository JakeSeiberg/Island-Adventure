using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class hungerBar : MonoBehaviour
{
    public static hungerBar instance;
    public GameObject hungerBarImage;

    private float physicalHungerLevel;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(loseHunger());
    }


    void FixedUpdate()
    {
        if (playerData.hungerValue >= 100)
        {
            physicalHungerLevel = 100;
        }
        else
        {
            physicalHungerLevel = playerData.hungerValue;
        }

        hungerBarImage.GetComponent<UnityEngine.UI.Image>().fillAmount = physicalHungerLevel / 100f;
        
        
    }

    private IEnumerator loseHunger()
    {
        yield return new WaitUntil(() => playerData.hasPlacedFish && playerData.hasBurnedWood);

        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (playerData.hungerValue > 0)
            {
                playerData.hungerValue -= .25f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerData.hungerValue = 0;
                toolTips.changeScene();
                playerData.curScene = "DeathScene";
                SceneManager.LoadScene("DeathScene");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (playerData.hungerValue < 70 && playerData.cookedFishCount > 0)
            {
                playerData.hungerValue += 100f;
                playerData.cookedFishCount -= 1;
                playerData.hasEatenFish = true;
            }
        }
    }
}
