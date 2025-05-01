using UnityEngine;
using System.Collections;

public class BoatManager : MonoBehaviour
{
    public GameObject hullPrefab;
    public GameObject sailPrefab;
    public GameObject motorPrefab;
    public GameObject gasPrefab;

    void Start()
    {
        hullPrefab.SetActive(false);
        sailPrefab.SetActive(false);
        motorPrefab.SetActive(false);
        gasPrefab.SetActive(false);
        StartCoroutine(boatCheck());
    }

    private IEnumerator boatCheck()
    {
        while (true)
        {
            if (playerData.boatHull)
            {
                hullPrefab.SetActive(true);
                if (playerData.boatSail)
                {
                    sailPrefab.SetActive(true);
                }
                if (playerData.boatMotor)
                {
                    motorPrefab.SetActive(true);
                }
                if (playerData.boatGas)
                {
                    gasPrefab.SetActive(true);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
