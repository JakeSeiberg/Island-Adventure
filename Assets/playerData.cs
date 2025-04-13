using UnityEngine;

public class playerData
{
    public static playerData Instance { get; private set; }

    public static Vector3 playerPosition;
    public static Vector3 playerRotation;

    public static bool startOfGame = true;

    public static int wormCount;
    public static int woodCount;
    public static int fishCount;
    public static int leafCount;

    public static bool hasSpear = false;

    public static bool hasPickedUpSpear = false;
    public static bool hasPickedUpAWorm = false;
    public static bool hasGoneFishing = false;
    public static bool hasThrownStrongSpear = false;
    public static bool hasEnteredTreeGame = false;
    public static bool hasPlayedTreeGame = false;
    public static bool hasAxe = false;

    public static string currentTreeID = null;
    public static bool treeChopped = false;

    public static string curScene = "MainWorld";
    //MainWorld, Fishing, Tree, 

    public playerData()
    {
        playerPosition = new Vector3(95f, 6f, 78f);
        //playerPosition = new Vector3(121.085f, 6.005f, 43.07f);
        playerRotation = new Vector3(-3f, -166f, 0f);
        startOfGame = true;
        wormCount = 0;
        woodCount = 0;
        fishCount = 0;
        leafCount = 0;

        hasSpear = false;

        hasPickedUpAWorm = false;
        hasPickedUpSpear = false;
        hasGoneFishing = false;
        hasThrownStrongSpear = false;
        hasEnteredTreeGame = false;
        hasPlayedTreeGame = false;
        hasAxe = false;

        currentTreeID = null;
        treeChopped = false;

        curScene = "MainWorld";
    }

    static playerData()
    {
        Instance = new playerData();
    }

    public static void newInstance()
    {
        Instance = new playerData();
    }
}