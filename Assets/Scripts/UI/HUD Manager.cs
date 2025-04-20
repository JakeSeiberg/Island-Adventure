using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public TMP_Text wormNum;
    public TMP_Text woodNum;
    public TMP_Text fishNum;
    public TMP_Text leafNum;
    public TMP_Text cookedFishNum;

    public TMP_Text fireVal;

    public TMP_Text spear;
    public TMP_Text axe;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void FixedUpdate()
    {
        wormNum.SetText(": " + playerData.wormCount.ToString());
        woodNum.SetText(": " + playerData.woodCount.ToString());
        fishNum.SetText(": " + playerData.fishCount.ToString());
        leafNum.SetText("leafNum: " + playerData.leafCount.ToString());
        //cookedFishNum.SetText("cookedFish: " + playerData.cookedFishCount.ToString());
        cookedFishNum.SetText("cookedFish: " + playerData.cookedFishCount.ToString());

        spear.SetText("Spear: " + playerData.hasSpear.ToString());
        axe.SetText("Axe: " + playerData.hasAxe.ToString());
        fireVal.SetText("Fire: " + playerData.fireValue.ToString());
    }
}
