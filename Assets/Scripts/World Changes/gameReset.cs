using UnityEngine;
using UnityEngine.SceneManagement;


public class GameReset : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("r clicked");
            playerData.newInstance();
            toolTips.changeScene();
            playerData.curScene = "MainWorld";
            SceneManager.LoadScene("MainWorld"); 
        }
    }
}
