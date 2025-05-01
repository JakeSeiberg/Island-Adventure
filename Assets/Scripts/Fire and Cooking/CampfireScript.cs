using UnityEngine;

public class CampfireScript : MonoBehaviour
{
    public GameObject fireOff;
    public GameObject fireOn;

    void FixedUpdate()
    {
        if (playerData.fireValue > 0)
        {
            fireOff.SetActive(false);
            fireOn.SetActive(true);
            playerData.fireBurning = true;
        }
        else
        {
            fireOff.SetActive(true);
            fireOn.SetActive(false);
            playerData.fireBurning = false;
        }
    }

    public static void interact()
    {
        if ((playerData.woodCount > 0) && playerData.fireValue <= 150)
        {
            playerData.fireValue += 50f;
            playerData.woodCount--;
        }       
    }
}
