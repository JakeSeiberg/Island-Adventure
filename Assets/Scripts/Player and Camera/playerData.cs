using UnityEngine;
using System.Collections;

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
    public static bool hasOpenedShop = false;
    public static bool hasPlacedFish = false;
    public static bool hasBurnedWood = false;
    public static bool hasCookedFish = false;
    public static bool hasEatenFish = false;
    public static bool hasEscaped = false;
    public static bool hasSlept = false;

    public static float fireValue = 0f;
    public static bool fireBurning = false;

    public static string currentTreeID = null;
    public static bool treeChopped = false;
    public static ArrayList brokenTrees;

    public static float[] fishTimers = new float[2];
    public static int[] fishStage = new int[2];
    public static int cookedFishCount;

    public static bool hasBoughtCampfire;
    public static bool hasBoughtShelter;
    public static bool hasBoughtBed;
    public static bool hasBoughtHull;

    public static float hungerValue;
    public static float air;

    public static bool day;

    public static bool boatHull;
    public static bool boatSail;
    public static bool boatMotor;
    public static bool boatGas;

    public static int curSkybox;
    public static bool canSleep;
    public static int sleepScore;

    public static bool toolTipsToggle;

    public static string curScene = "MainWorld";
    //MainWorld, Fishing, Tree, 

    public playerData()
    {
        //playerPosition = new Vector3(95f, 6f, 78f); //default
        //playerPosition = new Vector3(121.085f, 6.005f, 43.07f); //axe
        //playerPosition = new Vector3(94.22213f, 0.15230618f, 25.14625f); //campfire
        playerPosition = new Vector3(145.7728f, 0.8920119f, 43.1707f); //planeCrash
        playerRotation = new Vector3(16f, 88f, 0f);
        startOfGame = true;
        wormCount = 0;
        woodCount = 0;
        fishCount = 0;
        leafCount = 0;
        cookedFishCount = 0;

        hasSpear = false;

        hasPickedUpAWorm = false;
        hasPickedUpSpear = false;
        hasGoneFishing = false;
        hasThrownStrongSpear = false;
        hasEnteredTreeGame = false;
        hasPlayedTreeGame = false;
        hasAxe = false;
        hasOpenedShop = false;
        hasPlacedFish = false;
        hasBurnedWood = false;
        hasCookedFish = false;
        hasEatenFish = false;
        hasEscaped = false;
        hasSlept = false;

        currentTreeID = null;
        treeChopped = false;
        brokenTrees = new ArrayList();

        fishTimers[0] = float.NaN;
        fishTimers[1] = float.NaN;
        fishStage[0] = -1;
        fishStage[1] = -1;

        hasBoughtCampfire = false;
        hasBoughtShelter = false;
        hasBoughtBed = false;
        hasBoughtHull = false;

        boatHull = false;
        boatSail = false;
        boatMotor = false;
        boatGas = false;

        curSkybox = 0;
        canSleep = false;
        sleepScore = 0;

        curScene = "MainWorld";

        toolTipsToggle = true;

        hungerValue = 100f;
        air = 100f;
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