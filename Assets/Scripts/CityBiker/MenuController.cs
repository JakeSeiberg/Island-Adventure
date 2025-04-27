using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MenuController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer mixer;
    public void setVolume(){
        mixer.SetFloat("MasterVolume", volumeSlider.value);
    }
    
    public void OnPlayButton(){
        //toolTips.changeScene();
        Debug.Log("play Pressed");
        playerData.curScene = "CityBikerMain";
        SceneManager.LoadScene("CityBikerMain");
    }

    public void OnQuitButton(){
        Debug.Log("quit Pressed");
        //SceneManager.LoadScene(0);
        //toolTips.changeScene();
        playerData.curScene = "MainWorld";
        SceneManager.LoadScene("MainWorld"); 
    }

    public void OnPlayAgainButton(){
        //SceneManager.LoadScene(10);
        //toolTips.changeScene();
        playerData.curScene = "CityBikerMenu";
        SceneManager.LoadScene("CityBikerMenu");
    }

}
