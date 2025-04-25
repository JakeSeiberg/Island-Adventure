using UnityEngine;

public class ExplosionTimer : MonoBehaviour
{
    private float time;
    public AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - time > 12.97f){
            audioManager.playExplosion();
        }
    }
}
