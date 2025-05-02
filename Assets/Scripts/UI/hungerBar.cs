using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class hungerBar : MonoBehaviour
{
    public static hungerBar instance;
    public GameObject hungerBarImage;

    private float physicalHungerLevel;

    private float hungerTimer = 0f;
    private bool hungerActive = false;

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
        //StartCoroutine(loseHunger());
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

        if (hungerActive)
        {
            hungerTimer += Time.fixedDeltaTime;

            if (hungerTimer >= 1f)
            {
                hungerTimer = 0f;

                if (playerData.hungerValue > 0)
                {
                    if (playerData.curScene != "HALO")
                    {
                        playerData.hungerValue -= 0.25f;
                    }
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

        if (!hungerActive && playerData.hasGoneFishing)
        {
            hungerActive = true;
        }
    }
}
