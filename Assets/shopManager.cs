using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class shopManager : MonoBehaviour
{

    [Header("Resources Text")]
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI leafText;

    public Image campfireImage;
    public Image shelterImage;
    public Image bedImage;
    public Image hullImage;

    [Header("Purchased Item Color")]
    public Color purchasedColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    public bool canBuyCampfire = false;
    public bool canBuyShelter = false;
    public bool canBuyBed = false;
    public bool canBuyHull = false;

    public GameObject greyCampfire;
    public GameObject greyShelter;
    public GameObject greyBed;
    public GameObject greyHull;

    void Start()
    {
        UpdateUI();
        if (playerData.hasBoughtCampfire)
        {
            campfireImage.color = purchasedColor;
        }
    }

    void UpdateUI()
    {
        woodText.text = ": " + playerData.woodCount;
        leafText.text = ": " + playerData.leafCount;

    }

    public void buyCampfire()
    {
        // Check if the player has enough resources to buy the campfire
        if (canBuyCampfire)
        {
            playerData.woodCount -= 20;

            playerData.hasBoughtCampfire = true;
        }
        else
        {
            Debug.Log("Not enough resources to buy a campfire.");
        }
    }

    public void buyShelter()
    {
        // Check if the player has enough resources to buy the campfire
        if (canBuyShelter)
        {
            playerData.woodCount -= 20;
            playerData.leafCount -= 15;

            playerData.hasBoughtShelter = true;
        }
        else
        {
            Debug.Log("Not enough resources to buy a campfire.");
        }
    }
    
    //same thing but for bed, which costs 10 wood and 5 leaves
    public void buyBed()
    {
        if (canBuyBed)
        {
            playerData.woodCount -= 10;
            playerData.leafCount -= 5;

            playerData.hasBoughtBed = true;
        }
        else
        {
            Debug.Log("Not enough resources to buy a campfire.");
        }
    }

    //same for hull, which costs 40 wood
    public void buyHull()
    {
        if (canBuyHull)
        {
            playerData.woodCount -= 40;

            playerData.hasBoughtHull = true;
        }
        else
        {
            Debug.Log("Not enough resources to buy a campfire.");
        }
    }

    public void exit()
    {
        toolTips.changeScene();
        playerData.curScene = "MainWorld";
        SceneManager.LoadScene("MainWorld"); 
    }

    void FixedUpdate()
    {
        UpdateUI();

        if (playerData.woodCount >= 20 && !playerData.hasBoughtCampfire && playerData.hasBoughtBed)
        {
            canBuyCampfire = true;
            greyCampfire.SetActive(false);
        }
        else
        {
            canBuyCampfire = false;
            greyCampfire.SetActive(true);
        }
        if (playerData.woodCount >= 20 && playerData.leafCount >= 15 && !playerData.hasBoughtShelter && playerData.hasBoughtCampfire)
        {
            canBuyShelter = true;
            greyShelter.SetActive(false);
        }
        else
        {
            canBuyShelter = false;
            greyShelter.SetActive(true);
        }
        if (playerData.woodCount >= 10 && playerData.leafCount >= 5 && !playerData.hasBoughtBed)
        {
            canBuyBed = true;
            greyBed.SetActive(false);
        }
        else
        {
            canBuyBed = false;
            greyBed.SetActive(true);
        }
        if (playerData.woodCount >= 40 && !playerData.hasBoughtHull && playerData.hasBoughtShelter)
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
