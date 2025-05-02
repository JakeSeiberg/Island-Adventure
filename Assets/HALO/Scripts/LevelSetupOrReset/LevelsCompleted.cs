using System;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelsCompleted : MonoBehaviour
{
    public static bool[] levelsCompleted = {false, false, false, false, false, false, false};

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}

