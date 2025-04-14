using UnityEngine;
using System.Collections;

public class fishTimerRunner : MonoBehaviour
{

    private static fishTimerRunner instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Kill the new one
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(fishTimer());
        
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
}
