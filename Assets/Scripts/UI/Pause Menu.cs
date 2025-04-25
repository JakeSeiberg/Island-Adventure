using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameUI;
    private float time;

    void Start()
    {
        time = Time.time;
    }
    void Update()
    {
        if(Time.time - time > 0.25){
            menuCheck();
        }

        if(pauseMenu.activeSelf){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1;
        }
        
    }

    void menuCheck(){
        if(Input.GetKey(KeyCode.Escape) && pauseMenu.activeSelf){
            pauseMenu.SetActive(false);    
            time = Time.time;
        }
        else if(Input.GetKey(KeyCode.Escape) && !pauseMenu.activeSelf){
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            time = Time.time;
        }
        
    }

    public void close(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }
    public void quit()
    {
        Application.Quit();
    }
}
