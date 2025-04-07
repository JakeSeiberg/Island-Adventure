using UnityEngine;

public class playerData
{
    public static playerData Instance { get; private set; }

    public static Vector3 playerPosition;
    public static Vector3 playerRotation;

    public static int wormCount;
    public static int woodCount;
    public static int fishCount;

    public playerData()
    {
        playerPosition = new Vector3(95f, 6f, 78f);
        playerRotation = new Vector3(-3f, -166f, 0f);
        wormCount = 0;
        woodCount = 0;
        fishCount = 0;
    }

    static playerData()
    {
        Instance = new playerData();
    }
}