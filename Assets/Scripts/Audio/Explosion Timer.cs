using UnityEngine;

public class ExplosionTimer : MonoBehaviour
{
    private float time;
    public AudioManager audioManager;
    void Start()
    {
        time = Time.time;
    }

    void Update()
    {
        if(Time.time - time > 12.97f){
            audioManager.playExplosion();
        }
    }
}
