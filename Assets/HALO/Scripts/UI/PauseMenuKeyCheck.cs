using UnityEngine;

public class PauseMenuKeyCheck : MonoBehaviour
{

    public GameObject pauseMenu;
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
            time = Time.time;
        }
        
    }

}
