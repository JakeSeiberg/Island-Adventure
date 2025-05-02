using System;
using UnityEngine;

public class _AudioManager : MonoBehaviour
{
    public static _AudioManager Instance;
    public _Sound[] musicSounds, footstepSfx, deathSfx, doorSfx, lightSfx, monsterSfx, yawnSfx, levelCompleteSfx, menuClickSfx, menuLockSfx;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playMusic("Theme");
    }

    public void stopSfx(){
        sfxSource.Stop();
    }

    public void playMusic(string name){
        _Sound s = Array.Find(musicSounds, tmp => tmp.name == name);

        if(s == null){
            Debug.Log("Sound does not exist");
        }
        else{
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void playMenuClick(){
        _Sound s = menuClickSfx[0];

        if(s == null){
            Debug.Log("Sound does not exist");
        }
        else{
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void playFootstepSound(){
        int idx = UnityEngine.Random.Range(0,2);

        _Sound s = footstepSfx[idx];

        if(s == null){
            Debug.Log("Sound does not exist");
        }
        else{
            sfxSource.PlayOneShot(s.clip);
        }
        
    }

    public void playDeathSound(){
        int idx = UnityEngine.Random.Range(0,2);

        _Sound s = deathSfx[idx];

        if(s == null){
            Debug.Log("Sound does not exist");
        }
        else{
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void playMenuLockedLevel(){
        _Sound s = menuLockSfx[0];
        if(s == null){
            Debug.Log("Sound does not exist");
        }
        else{
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void playDoorSound(){
        int idx = UnityEngine.Random.Range(0,3);

        _Sound s = doorSfx[idx];

        if(s == null){
            Debug.Log("Sound does not exist");
        }
        else{
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void playLightSound(){

        _Sound s = lightSfx[0];

        if(s == null){
            Debug.Log("Sound does not exist");
        }
        else{
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void playMonsterSound(){
        int idx = UnityEngine.Random.Range(0,6);

        _Sound s = monsterSfx[idx];

        if(s == null){
            Debug.Log("Sound does not exist");
        }
        else{
            sfxSource.PlayOneShot(s.clip);
        }
        
    }

    public void playYawnSound(){
        int idx = UnityEngine.Random.Range(0,4);

        _Sound s = yawnSfx[idx];

        if(s == null){
            Debug.Log("Sound does not exist");
        }
        else{
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void playLevelComplete(){
        _Sound s = levelCompleteSfx[0];

        if(s == null){
            Debug.Log("Sound does not exist");
        }
        else{
            sfxSource.PlayOneShot(s.clip);
        }
    }
}

