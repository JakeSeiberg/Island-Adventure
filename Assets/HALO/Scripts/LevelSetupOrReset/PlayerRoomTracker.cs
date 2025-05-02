using UnityEngine;

public class PlayerRoomTracker : MonoBehaviour
{

    public static int playerCurrentRoomIndex;

    void Start()
    {
        playerCurrentRoomIndex = 0;
    }

    public static void SetPlayerRoomIndex(int roomIndex)
    {
        playerCurrentRoomIndex = roomIndex;
    }
}