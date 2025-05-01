using UnityEngine;

public class FireAudioCheck : MonoBehaviour
{
    public GameObject audioSrc;
    public GameObject fishAudioSrc;

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
