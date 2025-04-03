using UnityEngine;
using TMPro;

public class FishCounter : MonoBehaviour
{
    private TextMeshProUGUI fishCounterText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishCounterText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fishCounterText.text = $": {FishScript.fishHitCount}";

        
    }
}
