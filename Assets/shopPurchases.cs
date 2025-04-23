using UnityEngine;

public class shopPurchases : MonoBehaviour
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
                //hide gameobject
                gameObject.SetActive(false);
            }
        }
        else if (item == "Shelter")
        {
            if (!playerData.hasBoughtShelter)
            {
                //hide gameobject
                gameObject.SetActive(false);
            }
        }
        else if (item == "Bed")
        {
            if (!playerData.hasBoughtBed)
            {
                //hide gameobject
                gameObject.SetActive(false);
            }
        }
        else if (item == "Hull")
        {
            if (!playerData.hasBoughtHull)
            {
                //hide gameobject
                gameObject.SetActive(false);
            }
        }
    }

}