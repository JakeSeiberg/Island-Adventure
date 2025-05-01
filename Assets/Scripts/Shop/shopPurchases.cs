using UnityEngine;

public class ShopPurchases : MonoBehaviour
{

    public string item;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(true);
        if (item == "Campfire")
        {
            if (!playerData.hasBoughtCampfire)
            {
                gameObject.SetActive(false);
            }
        }
        else if (item == "Shelter")
        {
            if (!playerData.hasBoughtShelter)
            {
                gameObject.SetActive(false);
            }
        }
        else if (item == "Bed")
        {
            if (!playerData.hasBoughtBed)
            {
                gameObject.SetActive(false);
            }
        }
    }
}