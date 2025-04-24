using UnityEngine;
using System.Collections;

public class Drowning : MonoBehaviour
{
    private float air = 100f;
    private bool isDrowning = false;
    private float drowningYLevel = -2.8f;

    public GameObject airBarFull;
    public GameObject airBarEmpty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        airBarFull.SetActive(false);
        airBarEmpty.SetActive(false);
    }


    void FixedUpdate()
    {
        //Debug.Log(isDrowning);
        //Debug.Log("Air Level: " + air);
        if (PlayerMovement.currentPlayerPos.y < drowningYLevel)
        {
            if (!isDrowning)
            {
                StartCoroutine(IsDrowning());
                isDrowning = true;
            }
        }
        else
        {
            isDrowning = false;
            air = 100f; 
        }
    }

    private IEnumerator IsDrowning()
    {
        while (PlayerMovement.currentPlayerPos.y < drowningYLevel)
        {
            airBarFull.GetComponent<UnityEngine.UI.Image>().fillAmount = air / 100f;
            airBarFull.SetActive(true);
            airBarEmpty.SetActive(true);
            // Decrease air over 15 seconds
            yield return new WaitForSeconds(0.15f); // 15 seconds / 100 air = 0.15 seconds per decrement
            air -= 1f;

            if (air <= 0)
            {
                Debug.Log("Player has died due to drowning.");
                yield break; 
            }

            


        }

        air = 100f;
        isDrowning = false;
        //hide airBar gameobject
        airBarFull.SetActive(false);
        airBarEmpty.SetActive(false);


    }
}
