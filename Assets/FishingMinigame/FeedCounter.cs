using UnityEngine;
using TMPro;

public class FeedCounter : MonoBehaviour
{
    private TextMeshProUGUI feedCounterText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        feedCounterText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        feedCounterText.text = $": {playerData.wormCount}";

        
    }
}
