using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public TMP_Text wormNum;
    public TMP_Text woodNum;
    public TMP_Text fishNum;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void FixedUpdate()
    {
        wormNum.SetText("Worm: " + playerData.wormCount.ToString());
        woodNum.SetText("Wood: " + playerData.woodCount.ToString());
        fishNum.SetText("Fish: " + playerData.fishCount.ToString());
    }

}
