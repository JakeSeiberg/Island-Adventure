using UnityEngine;

public class InteractionWorm : MonoBehaviour
{
    bool collide = false;
    void Update()
    {
        if(collide && (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Slash))){
            //whatever you want to happen when interacting with a worm goes here
        }
    }
    void OnTriggerEnter()
    {
        collide = true;
    }

    void OnTriggerExit()
    {
        collide = false;
    }
}
