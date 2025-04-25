using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;

public class sceneSwitcher : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Cur scene: " + playerData.curScene);
        //when escape is pressed, switch scene to "TYLER NEW SCENE"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (playerData.curScene != "CutScene")
            {
                toolTips.changeScene();
            }
            playerData.curScene = "MainWorld";
            SceneManager.LoadScene("MainWorld");
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