using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{

    public TMP_Text wormNum;
    public TMP_Text woodNum;
    public TMP_Text fishNum;
    public TMP_Text leafNum;
    public TMP_Text cookedFishNum;

    public TMP_Text spear;
    public TMP_Text axe;

    void FixedUpdate()
    {
        wormNum.SetText(": " + playerData.wormCount.ToString());
        woodNum.SetText(": " + playerData.woodCount.ToString());
        fishNum.SetText(": " + playerData.fishCount.ToString());
        leafNum.SetText(": " + playerData.leafCount.ToString());
        cookedFishNum.SetText(": " + playerData.cookedFishCount.ToString());

        spear.SetText("Spear: " + playerData.hasSpear.ToString());
        axe.SetText("Axe: " + playerData.hasAxe.ToString());
    }
}
