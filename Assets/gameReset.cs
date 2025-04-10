using UnityEngine;
using UnityEngine.SceneManagement;


public class gameReset : MonoBehaviour
{
    private static gameReset instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if r key pressed, reset player data
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
