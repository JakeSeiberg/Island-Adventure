using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public TMP_Text wormNum;
    public TMP_Text woodNum;
    public TMP_Text fishNum;
    public TMP_Text spear;
    public TMP_Text axe;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void FixedUpdate()
    {
        wormNum.SetText(": " + playerData.wormCount.ToString());
        woodNum.SetText(": " + playerData.woodCount.ToString());
        fishNum.SetText(": " + playerData.fishCount.ToString());
        spear.SetText("Spear: " + playerData.hasSpear.ToString());
        axe.SetText("Axe: " + playerData.hasAxe.ToString());
    }

}
