using UnityEngine;
using System.Collections;

public class fishTimerRunner : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(fishTimer());
        StartCoroutine(fireTimer());
        
    }

    private IEnumerator fishTimer()
    {
        while (true)
        {
            if (playerData.fireBurning)
            {
                if (!float.IsNaN(playerData.fishTimers[0]))
                {
                    playerData.fishTimers[0] += Time.deltaTime;
                }
                if (!float.IsNaN(playerData.fishTimers[1]))
                {
                    playerData.fishTimers[1] += Time.deltaTime;
                }
            }
            
            yield return null;
        }
    }

    private IEnumerator fireTimer()
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
}
