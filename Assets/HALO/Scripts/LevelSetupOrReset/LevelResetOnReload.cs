using UnityEngine;

public class LevelResetOnReload : MonoBehaviour
{
    void Start()
    {
        LeverTracker.resetLevers();
        DisplayShift.resetBools();
        PlayerRoomTracker.SetPlayerRoomIndex(0);

    }

}
