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


