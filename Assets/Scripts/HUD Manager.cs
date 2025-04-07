using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static int wormCount;
    public static int woodCount;
    public static int fishCount;

    public TMP_Text wormNum;
    public TMP_Text woodNum;
    public TMP_Text fishNum;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wormCount = 0;
        woodCount = 0;
        fishCount = 0;
    }

    void Update()
    {
        wormNum.SetText("Worm: " + wormCount.ToString());
        woodNum.SetText("Wood: " + woodCount.ToString());
        fishNum.SetText("Fish: " + fishCount.ToString());
    }

}
