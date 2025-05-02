using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneSwitcher : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (playerData.curScene != "SleepScene")
            {
                if (playerData.curScene != "CutScene")
                {
                    toolTips.changeScene();
                }
                playerData.curScene = "MainWorld";
                SceneManager.LoadScene("MainWorld");
            }
        }
    }

    public static void changeScene()
    {
        if (playerData.curScene != "CutScene")
        {
            toolTips.changeScene();
        }
        playerData.curScene = "MainWorld";
        SceneManager.LoadScene("MainWorld"); 
    }
}