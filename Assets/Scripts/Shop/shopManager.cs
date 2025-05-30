using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{

    [Header("Resources Text")]
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI leafText;

    public Image campfireImage;
    public Image shelterImage;
    public Image bedImage;
    public Image hullImage;

    public bool canBuyCampfire = false;
    public bool canBuyShelter = false;
    public bool canBuyBed = false;
    public bool canBuyHull = false;

    public GameObject greyCampfire;
    public GameObject greyShelter;
    public GameObject greyBed;
    public GameObject greyHull;

    public AudioManager audioManager;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
//        Debug.Log(Sleep score"")
    }

    void UpdateUI()
    {
        woodText.text = ": " + playerData.woodCount;
        leafText.text = ": " + playerData.leafCount;

    }

    public void buyCampfire()
    {
        Debug.Log("Attempting to buy campfire");
        if (canBuyCampfire && playerData.woodCount >= 20)
        {
            playerData.woodCount -= 20;

            playerData.hasBoughtCampfire = true;
        }
        else
        {
            audioManager.playBuzzer();
            Debug.Log("Not enough resources to buy a campfire.");
        }
    }

    public void buyShelter()
    {
        Debug.Log("Attempting to buy shelter");
        if (canBuyShelter && playerData.woodCount >= 20 && playerData.leafCount >= 15)
        {
            playerData.woodCount -= 20;
            playerData.leafCount -= 15;

            playerData.hasBoughtShelter = true;
        }
        else
        {
            audioManager.playBuzzer();
            Debug.Log("Not enough resources to buy a campfire.");
        }
    }
    
    public void buyBed()
    {
        Debug.Log("Attempting to buy bed");
        if (canBuyBed && playerData.woodCount >= 10 && playerData.leafCount >= 5)
        {
            playerData.woodCount -= 10;
            playerData.leafCount -= 5;

            playerData.hasBoughtBed = true;
            playerData.sleepScore = 100;
        }
        else
        {
            audioManager.playBuzzer();
            Debug.Log("Not enough resources to buy a campfire.");
        }
    }

    public void buyHull()
    {
        Debug.Log("Attempting to buy hull");
        if (canBuyHull && playerData.woodCount >= 40)
        {
            playerData.woodCount -= 40;

            playerData.hasBoughtHull = true;
            playerData.boatHull = true;
        }
        else
        {
            audioManager.playBuzzer();
            Debug.Log("Not enough resources to buy a campfire.");
        }
    }

    public void exit()
    {
        SceneSwitcher.changeScene();
    }

    void FixedUpdate()
    {
        UpdateUI();

        if (!playerData.hasBoughtCampfire && playerData.hasBoughtBed)
        {
            canBuyCampfire = true;
            greyCampfire.SetActive(false);
        }
        else
        {
            canBuyCampfire = false;
            greyCampfire.SetActive(true);
        }

        if (!playerData.hasBoughtShelter && playerData.hasBoughtCampfire)
        {
            canBuyShelter = true;
            greyShelter.SetActive(false);
        }
        else
        {
            canBuyShelter = false;
            greyShelter.SetActive(true);
        }

        if (!playerData.hasBoughtBed)
        {
            canBuyBed = true;
            greyBed.SetActive(false);
        }
        else
        {
            canBuyBed = false;
            greyBed.SetActive(true);
        }

        if (!playerData.hasBoughtHull && playerData.hasBoughtShelter)
        {
            canBuyHull = true;
            greyHull.SetActive(false);
        }
        else
        {
            canBuyHull = false;
            greyHull.SetActive(true);
        }
    }
}
