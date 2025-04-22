using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    void Start()
    {
        UpdateUI();
        if (playerData.hasBoughtCampfire)
        {
            campfireImage.color = purchasedColor;
        }
    }

    void FixedUpdate()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        woodText.text = ": " + playerData.woodCount;
        leafText.text = ": " + playerData.leafCount;

    }

    public void buyCampfire()
    {
        // Check if the player has enough resources to buy the campfire
        if (playerData.woodCount >= 20 && !playerData.hasBoughtCampfire)
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
        if (playerData.woodCount >= 20 && playerData.leafCount >= 15 && !playerData.hasBoughtShelter)
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
        if (playerData.woodCount >= 10 && playerData.leafCount >= 5 && !playerData.hasBoughtBed)
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
        if (playerData.woodCount >= 40 && !playerData.hasBoughtHull)
        {
            playerData.woodCount -= 40;

            playerData.hasBoughtHull = true;
        }
        else
        {
            Debug.Log("Not enough resources to buy a campfire.");
        }
    }


}
