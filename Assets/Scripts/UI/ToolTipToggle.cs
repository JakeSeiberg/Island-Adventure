using UnityEngine;
using UnityEngine.UI;

public class ToolTipToggle : MonoBehaviour
{
    public Toggle toolTipsToggle;

    void Start()
    {
        // Initialize toggle based on playerData
        toolTipsToggle.isOn = playerData.toolTipsToggle;

        // Add listener for when the toggle changes
        toolTipsToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        playerData.toolTipsToggle = isOn;
    }

    void OnDestroy()
    {
        toolTipsToggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}
