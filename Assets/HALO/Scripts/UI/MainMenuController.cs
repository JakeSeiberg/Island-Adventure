using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    static public int curLevel;
    static public bool[] levelsCompleted = {false, false, false, false, false, false, false};
    
    void Start()
    {
        GameObject ddolObject = GameObject.Find("DontDestroyOnLoad");
        ddolObject.SetActive(false);
    }
    
    public void OnQuit(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if (go.name == "DontDestroyOnLoad")
            {
                go.SetActive(true); // Or whatever you want to do with it
            }
        }

        GameObject delete = GameObject.Find("LevelTracker");
        Destroy(delete);
        GameObject delete2 = GameObject.Find("AudioManager");
        Destroy(delete2);
    

        toolTips.changeScene();
        playerData.curScene = "MainWorld";
        SceneManager.LoadScene("MainWorld"); 
    }

    public void OnTutorial(){
        curLevel = 3;
        _AudioManager.Instance.playMenuClick();
        SceneManager.LoadScene(2);
    }

    public void OnLeveOne(){
        curLevel = 4;
        _AudioManager.Instance.playMenuClick();
        SceneManager.LoadScene(4);
    }

    public void OnLevelTwo()
    {
        if (LevelsCompleted.levelsCompleted[4])
        {
            curLevel = 5;
            _AudioManager.Instance.playMenuClick();
            SceneManager.LoadScene(5);
        }
        else{
            _AudioManager.Instance.playMenuLockedLevel();
        }
    }

    public void OnLevelThree(){
        if (LevelsCompleted.levelsCompleted[5])
        {
            curLevel = 6;
            _AudioManager.Instance.playMenuClick();
            SceneManager.LoadScene(6);
        }
        else{
            _AudioManager.Instance.playMenuLockedLevel();
        }
        
    }

}
