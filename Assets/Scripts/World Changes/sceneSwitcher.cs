using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;

public class sceneSwitcher : MonoBehaviour
{
    public SkyboxChanger SkyboxChanger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //when escape is pressed, switch scene to "TYLER NEW SCENE"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toolTips.changeScene();
            playerData.curScene = "MainWorld";
            SceneManager.LoadScene("MainWorld");
            if (playerData.hasBoughtBed && SkyboxChanger != null){
                SkyboxChanger.ChangeSkybox();
            }
        }
    }

    public static void changeScene()
    {
        toolTips.changeScene();
        playerData.curScene = "MainWorld";
        SceneManager.LoadScene("MainWorld"); 
    }
}