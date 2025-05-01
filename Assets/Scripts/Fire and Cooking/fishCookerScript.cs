using UnityEngine;
using System.Collections;

public class FishCookerScript : MonoBehaviour
{
    public GameObject LeftFish;
    public GameObject RightFish;

    public GameObject leftTimer;
    public GameObject rightTimer;

    private float fishTimerLength;

    public Material rawFish;
    public Material partiallyCookedFish;
    public Material cookedFish;
    public Material burntFish;

    void Start()
    {
        fishTimerLength = 20f;
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
        if (playerData.fishStage[0] == -1)
        {
            playerData.fishTimers[0] = 0;
            playerData.fishStage[0] = 0;
            leftTimer.transform.rotation = Quaternion.Euler(0f, 0f, -298.3f);
        }
        else if (playerData.fishStage[1] == -1)
        {
            playerData.fishTimers[1] = 0;
            playerData.fishStage[1] = 0;
            rightTimer.transform.rotation = Quaternion.Euler(0f, 0f, -298.3f);
        }
    }

    public void fishLeft()
    {   
        if (playerData.fishStage[0] == 1) 
        {
            playerData.fishStage[0] = 2;
            StartCoroutine(startFlip(0, 1f));
            playerData.fishTimers[0] = 0;
        }
        else if (playerData.fishStage[0] == 3)
        {
            playerData.fishStage[0] = -1;
            playerData.cookedFishCount++;
            playerData.fishTimers[0] = float.NaN;
            playerData.hasCookedFish = true;
        }
        else
        {
            playerData.fishStage[0] = -1;
            playerData.fishTimers[0] = float.NaN;
        }
    }

    public void fishRight()
    {
        if (playerData.fishStage[1] == 1) 
        {
            playerData.fishStage[1] = 2;
            StartCoroutine(startFlip(1, 1f));
            playerData.fishTimers[1] = 0;
        }
        else if (playerData.fishStage[1] == 3)
        {
            playerData.fishStage[1] = -1;
            playerData.cookedFishCount++;
            playerData.fishTimers[1] = float.NaN;
            playerData.hasCookedFish = true;
        }
        else
        {
            playerData.fishStage[1] = -1;
            playerData.fishTimers[1] = float.NaN;

        }
    }

    public static int fishOn()
    {
        int fishies = 0;
        if (playerData.fishStage[0] != -1)
        {
            fishies++;
        }
        if (playerData.fishStage[1] != -1)
        {
            fishies++;
        }

        return fishies;
    }

    public IEnumerator startFlip(int fishVal, float duration = 1f)
    {
        GameObject fish;
        if (fishVal == 0)
        {
            fish = LeftFish;
        }
        else
        {
            fish = RightFish;
        }
        Vector3 startPos = fish.transform.localPosition;
        Vector3 startEuler = fish.transform.localEulerAngles;

        Quaternion startRot = Quaternion.Euler(startEuler);
        Quaternion endRot;

        if (fishVal == 0)
        {
            endRot = Quaternion.Euler(startEuler.x, startEuler.y, startEuler.z + 180f);
        }
        else
        {
            endRot = Quaternion.Euler(startEuler.x, startEuler.y, startEuler.z - 180f);
        }

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            float curveT = Mathf.SmoothStep(0f, 1f, t);

            float yOffset = Mathf.Sin(curveT * Mathf.PI) * 0.286f;
            fish.transform.localPosition = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);

            fish.transform.localRotation = Quaternion.Slerp(startRot, endRot, curveT);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fish.transform.localPosition = startPos;
        fish.transform.localRotation = endRot;
    }

    void FixedUpdate()
    {

        if (playerData.fishTimers[0] > ((56.35024f / 60) * fishTimerLength))
        {
            playerData.fishStage[0] = 4; 
        }
        else if (playerData.fishTimers[0] > ((49.02712f / 60) * fishTimerLength) && playerData.fishTimers[0] < (56.35024f / 60) * fishTimerLength)
        {
            if (playerData.fishStage[0] == 0)
            {
                playerData.fishStage[0] = 1;
            }
            else if (playerData.fishStage[0] == 2)
            {
                playerData.fishStage[0] = 3;
            }
        }

        if (playerData.fishTimers[1] > ((56.35024f / 60) * fishTimerLength))
        {
            playerData.fishStage[1] = 4;
        }
        else if (playerData.fishTimers[1] > ((49.02712f / 60) * fishTimerLength) && playerData.fishTimers[1] < (56.35024f / 60) * fishTimerLength)
        {
            if (playerData.fishStage[1] == 0)
            {
                playerData.fishStage[1] = 1;
            }
            else if (playerData.fishStage[1] == 2)
            {
                playerData.fishStage[1] = 3;
            }
        }
        
        if (playerData.fireBurning)
        {
            if (playerData.fishStage[0] != -1)
            {
                float zRotation = Mathf.Lerp(-298.3f, 0.93f, playerData.fishTimers[0] / fishTimerLength);
                leftTimer.transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
            }
            if (playerData.fishStage[1] != -1)
            {
                float zRotation = Mathf.Lerp(-298.3f, 0.93f, playerData.fishTimers[1] / fishTimerLength);
                rightTimer.transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
            }
        }

        switch (playerData.fishStage[0])
        {
            case -1:
                LeftFish.GetComponentInChildren<Renderer>().material = rawFish;
                LeftFish.SetActive(false);
                leftTimer.transform.parent.gameObject.SetActive(false);
                break;
            case 0:
                LeftFish.GetComponentInChildren<Renderer>().material = rawFish;
                LeftFish.SetActive(true);
                leftTimer.transform.parent.gameObject.SetActive(true);
                break;

            case 1:
                LeftFish.GetComponentInChildren<Renderer>().material = partiallyCookedFish;
                LeftFish.SetActive(true);
                leftTimer.transform.parent.gameObject.SetActive(true);
                break;

            case 2:
                LeftFish.GetComponentInChildren<Renderer>().material = partiallyCookedFish;
                LeftFish.SetActive(true);
                leftTimer.transform.parent.gameObject.SetActive(true);
                break;

            case 3:
                LeftFish.GetComponentInChildren<Renderer>().material = cookedFish;
                LeftFish.SetActive(true);
                leftTimer.transform.parent.gameObject.SetActive(true);
                break;

            case 4:
                LeftFish.GetComponentInChildren<Renderer>().material = burntFish;
                LeftFish.SetActive(true);
                leftTimer.transform.parent.gameObject.SetActive(true);
                break;
            default:
                break;
        }

        switch (playerData.fishStage[1])
        {
            case -1:
                RightFish.GetComponentInChildren<Renderer>().material = rawFish;
                RightFish.SetActive(false);
                rightTimer.transform.parent.gameObject.SetActive(false);
                break;
            case 0:
                RightFish.GetComponentInChildren<Renderer>().material = rawFish;
                RightFish.SetActive(true);
                rightTimer.transform.parent.gameObject.SetActive(true);
                break;
            case 1:
                RightFish.GetComponentInChildren<Renderer>().material = partiallyCookedFish;
                RightFish.SetActive(true);
                rightTimer.transform.parent.gameObject.SetActive(true);
                break;

            case 2:
                RightFish.GetComponentInChildren<Renderer>().material = partiallyCookedFish;
                RightFish.SetActive(true);
                rightTimer.transform.parent.gameObject.SetActive(true);
                break;
            case 3:
                RightFish.GetComponentInChildren<Renderer>().material = cookedFish;
                RightFish.SetActive(true);
                rightTimer.transform.parent.gameObject.SetActive(true);
                break;
            case 4:
                RightFish.GetComponentInChildren<Renderer>().material = burntFish;
                RightFish.SetActive(true);
                rightTimer.transform.parent.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
