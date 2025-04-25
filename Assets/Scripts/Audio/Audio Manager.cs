using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public Sound[] sound;
    public AudioSource audioSrc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void playAxe(){

        if(sound == null){
            Debug.Log("sound does not exist");
        }
        else{
            audioSrc.clip = sound[0].clip;
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }

    public void playTree(){
        if(sound == null){
            Debug.Log("sound does not exist");
        }
        else{
            audioSrc.clip = sound[1].clip;
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }

    public void playSplash(){
        if(sound == null){
            Debug.Log("sound does not exist");
        }
        else{
            audioSrc.clip = sound[2].clip;
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }

    public void playGrunt(){
        if(sound == null){
            Debug.Log("sound does not exist");
        }
        else{
            audioSrc.clip = sound[3].clip;
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }

    public void playExplosion(){
        if(sound == null){
            Debug.Log("sound does not exist");
        }
        else{
            audioSrc.clip = sound[4].clip;
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }

}
