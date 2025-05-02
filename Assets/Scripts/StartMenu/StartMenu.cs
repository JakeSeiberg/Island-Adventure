using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        if (playerData.curScene == "DeathScene")
        {
            GameObject destroy = GameObject.FindGameObjectWithTag("DontDestroyOnLoad");
            Destroy(destroy);
        }

        GameObject destroy2 = GameObject.Find("DontDestroyOnLoad");
        if (destroy2 != null)
        {
            Destroy(destroy2);
        }
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("CutScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void RestartGame()
    {
        playerData.newInstance();
        toolTips.changeScene();
        playerData.curScene = "CutScene";
        SceneManager.LoadScene("CutScene"); 
    }
}


