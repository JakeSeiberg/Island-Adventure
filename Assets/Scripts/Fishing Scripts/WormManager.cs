using UnityEngine;
using System.Collections;

public class WormManager : MonoBehaviour
{
    public GameObject wormPrefab; 
    private bool canSpawnWorms = true;
    public static float wormMultiplier = 1f;
    public static float wormSpawnMultiplier = 1f;

    public FishManager fishManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSpawnWorms && playerData.wormCount > 0)
        {
            StartCoroutine(summonWorms());
            canSpawnWorms = false;

        }
    }

    private IEnumerator summonWorms()
    {
        int wormsThrowing;
        if (playerData.wormCount > 3)
        {
            wormsThrowing = 3;
        }
        else
        {
            wormsThrowing = playerData.wormCount;
        }
        playerData.wormCount -= wormsThrowing;
        StartCoroutine(wormBoost(wormsThrowing));

        GameObject worm1 = Instantiate(wormPrefab);
        PhysicalWormScript wormScript1 = worm1.GetComponent<PhysicalWormScript>();
        if (wormScript1 != null)
        {
            wormScript1.Initialize(-30f, -15f); 
        }
        wormsThrowing--;
        yield return new WaitForSeconds(.2f);

        if (wormsThrowing > 0)
        {
            GameObject worm2 = Instantiate(wormPrefab);
            PhysicalWormScript wormScript2 = worm2.GetComponent<PhysicalWormScript>();
            if (wormScript2 != null)
            {
                wormScript2.Initialize(-15f, 15f);
            }
            yield return new WaitForSeconds(.2f);
            wormsThrowing--;
        }

        if (wormsThrowing > 0)
        {
            GameObject worm3 = Instantiate(wormPrefab);
            PhysicalWormScript wormScript3 = worm3.GetComponent<PhysicalWormScript>();
            if (wormScript3 != null)
            {
                wormScript3.Initialize(15f, 30f);
            }
        }
    }

    private IEnumerator wormBoost(int wormsThrowing)
    {
        wormMultiplier = (wormsThrowing * 5/3f);
        wormSpawnMultiplier = 2f;
        fishManager.restartFishSpawning();
        yield return new WaitForSeconds(12f);
        wormMultiplier = 1f;
        wormSpawnMultiplier = 1f;
        canSpawnWorms = true;
    }
    
}
