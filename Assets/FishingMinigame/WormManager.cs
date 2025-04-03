using UnityEngine;
using System.Collections;

public class WormManager : MonoBehaviour
{
    public GameObject wormPrefab; // Prefab for the worm
    private bool canSpawnWorms = true;
    public static int wormCount = 5;
    public static float wormMultiplier = 1f;
    public static float wormSpawnMultiplier = 1f;

    public FishManager fishManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSpawnWorms && wormCount > 0)
        {
            StartCoroutine(summonWorms());
            StartCoroutine(wormBoost());
            wormCount--;
            canSpawnWorms = false;

        }
    }

    private IEnumerator summonWorms()
    {
        GameObject worm1 = Instantiate(wormPrefab);
        PhysicalWormScript wormScript1 = worm1.GetComponent<PhysicalWormScript>();
        if (wormScript1 != null)
        {
            wormScript1.Initialize(-30f, -15f); // Set min and max angles
        }
        yield return new WaitForSeconds(.2f);

        GameObject worm2 = Instantiate(wormPrefab);
        PhysicalWormScript wormScript2 = worm2.GetComponent<PhysicalWormScript>();
        if (wormScript2 != null)
        {
            wormScript2.Initialize(-15f, 15f); // Set min and max angles
        }
        yield return new WaitForSeconds(.2f);

        GameObject worm3 = Instantiate(wormPrefab);
        PhysicalWormScript wormScript3 = worm3.GetComponent<PhysicalWormScript>();
        if (wormScript3 != null)
        {
            wormScript3.Initialize(15f, 30f); // Set min and max angles
        }
    }

    private IEnumerator wormBoost()
    {
        wormMultiplier = 5f;
        wormSpawnMultiplier = 2f;
        fishManager.restartFishSpawning();
        yield return new WaitForSeconds(12f);
//        Debug.Log("Worm boost ended");
        wormMultiplier = 1f;
        wormSpawnMultiplier = 1f;
        canSpawnWorms = true;
    }
    
}
