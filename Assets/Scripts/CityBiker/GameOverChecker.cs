using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverChecker : MonoBehaviour
{
    static public int score;
    private float tmpTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;   
        tmpTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
        checkDead();
    }

    void updateScore(){
        if(Time.time - tmpTime >= 1){
            tmpTime = Time.time;
            score++;
        }
    }

    void checkDead(){
        if(Timer.currentTime <= 0){
            Timer.currentTime = 60;

            toolTips.changeScene();
            playerData.curScene = "CityBikerGame Over";
            SceneManager.LoadScene("CityBikerGame Over");
        }
    }
}
