using UnityEngine;
using System.Collections;

public class hungerBar : MonoBehaviour
{
    public static hungerBar Instance;
    public GameObject hungerBarImage;

    private float physicalHungerLevel;

    void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy this GameObject if another instance exists
            return;
        }

        // Set this as the Singleton instance
        Instance = this;

        // Make this GameObject persist across scene loads
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoseHunger());
    }

    // Update is called once per frame
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

        //make the fill amount of the hunger bar equal to the hunger value
        hungerBarImage.GetComponent<UnityEngine.UI.Image>().fillAmount = physicalHungerLevel / 100f;
        
    }

    private IEnumerator LoseHunger()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (playerData.hungerValue > 0)
            {
                playerData.hungerValue -= .5f;
            }
            else
            {
                playerData.hungerValue = 0;
            }
            //Debug.Log("Hunger value: " + playerData.hungerValue);
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
            }
        }
    }
}
