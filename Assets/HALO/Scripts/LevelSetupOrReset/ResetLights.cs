using UnityEngine;

public class ResetLights : MonoBehaviour
{
    private bool hasReset = false;

    void Update()
    {
        if (!hasReset && LeverTracker.leversSwitched != null)
        {
            LeverTracker.resetLevers();
            hasReset = true;
        }
    }
}