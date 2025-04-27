using UnityEngine;

public class FireAudioCheck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject audioSrc;
    public GameObject fishAudioSrc;

    // Update is called once per frame
    void Update()
    {
        if(playerData.fireBurning){
            audioSrc.SetActive(true);
        }
        else{
            audioSrc.SetActive(false);
        }

        if(playerData.hasPlacedFish){
            fishAudioSrc.SetActive(true);
        }
        else{
            fishAudioSrc.SetActive(false);
        }
    }
}
