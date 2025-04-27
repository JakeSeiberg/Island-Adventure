using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    static public float currentTime = 60;

    private RedBox box;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box = FindFirstObjectByType<RedBox>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        timerText.text = currentTime.ToString("F0");
    }

    public void loseTime(int seconds)
    {
        currentTime -= seconds;
        if (seconds > 0)
        {
            box.takeDamage();
        }
    }

}
