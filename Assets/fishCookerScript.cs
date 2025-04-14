using UnityEngine;
using System.Collections;

public class fishCookerScript : MonoBehaviour
{
    public GameObject LeftFish;
    public GameObject RightFish;

    public GameObject leftTimer;
    public GameObject rightTimer;

    private float fishTimerLength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isActive(0))
        {
            LeftFish.SetActive(true);
            enable(0);
        }
        else
        {
            LeftFish.SetActive(false);
            disable(0);
        }
        if (isActive(1))
        {
            RightFish.SetActive(true);
            enable(1);
        }
        else
        {
            RightFish.SetActive(false);
            disable(1);
        }
        fishTimerLength = 60f;
        StartCoroutine(fishTimer());
    }

    void OnDisable()
    {
        playerData.lastFishCheckTime = Time.realtimeSinceStartup;
    }

    public void interact()
    {
        if (fishOn() < 2 && playerData.fishCount > 0)
        {
            playerData.fishCount -= 1;
            putFishOn();
        }
    }

    public void putFishOn()
    {
        if (!isActive(0))
        {
            playerData.fishTimers[0] = 0;
            LeftFish.SetActive(true);
            enable(0);
            leftTimer.transform.rotation = Quaternion.Euler(0f, 0f, -298.3f);
        }
        else if (!isActive(1))
        {
            playerData.fishTimers[1] = 0;
            RightFish.SetActive(true);
            enable(1);
            rightTimer.transform.rotation = Quaternion.Euler(0f, 0f, -298.3f);
        }
    }

    public void fishLeft()
    {

        if (playerData.fishTimers[0] > 49.02712f && playerData.fishTimers[0] < 56.35024f)
        {
            playerData.cookedFishCount += 1;
        }
        disable(0);
        leftTimer.transform.parent.gameObject.SetActive(false);
        LeftFish.SetActive(false);
        playerData.fishTimers[0] = float.NaN;
    }

    public void fishRight()
    {

        if (playerData.fishTimers[1] > 49.02712f && playerData.fishTimers[1] < 56.35024f)
        {
            playerData.cookedFishCount += 1;
        }
        disable(1);
        rightTimer.transform.parent.gameObject.SetActive(false);
        RightFish.SetActive(false);
        playerData.fishTimers[1] = float.NaN;
    }

    public void disable(int fishNum)
    {
        if (fishNum == 0)
        {
            leftTimer.transform.parent.gameObject.SetActive(false);
        }
        else if (fishNum == 1)
        {
            rightTimer.transform.parent.gameObject.SetActive(false);
        }
    }

    public void enable(int fishNum)
    {
        if (fishNum == 0)
        {
            leftTimer.transform.parent.gameObject.SetActive(true);
        }
        else if (fishNum == 1)
        {
            rightTimer.transform.parent.gameObject.SetActive(true);
        }
    }

    public static int fishOn()
    {
        int fishies = 0;
        if (isActive(0))
        {
            fishies++;
        }
        if (isActive(1))
        {
            fishies++;
        }

        return fishies;
    }


    public static bool isActive(int fishNum)
    {
        if (!float.IsNaN(playerData.fishTimers[fishNum]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator fishTimer()
    {
        while (true)
        {
            if (playerData.fireBurning)
            {
                if (isActive(0))
                {
                    float zRotation = Mathf.Lerp(-298.3f, 0.93f, playerData.fishTimers[0] / fishTimerLength);
                    leftTimer.transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
                }
                if (isActive(1))
                {
                    float zRotation = Mathf.Lerp(-298.3f, 0.93f, playerData.fishTimers[1] / fishTimerLength);
                    rightTimer.transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
                }
            }

            yield return null;
        }
    }
}
