using UnityEngine;
using System.Collections;

public class BoatManager : MonoBehaviour
{
    public GameObject hullPrefab;
    public GameObject sailPrefab;
    public GameObject motorPrefab;
    public GameObject gasPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hullPrefab.SetActive(false);
        sailPrefab.SetActive(false);
        motorPrefab.SetActive(false);
        gasPrefab.SetActive(false);
        StartCoroutine(BoatCheck());
    }

    private IEnumerator BoatCheck()
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
