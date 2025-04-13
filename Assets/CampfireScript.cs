using UnityEngine;
using System.Collections;

public class CampfireScript : MonoBehaviour
{
    public GameObject fireOff;
    public GameObject fireOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(burnFire());
    }

    // Update is called once per frame
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

    private IEnumerator burnFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (playerData.fireValue > 0)
            {
                playerData.fireValue -= 1f;
            }
        }
    }

    public static void interact()
    {
        if (playerData.woodCount > 0)
        {
            playerData.fireValue += 30f;
            playerData.woodCount--;
        }       
    }
}
