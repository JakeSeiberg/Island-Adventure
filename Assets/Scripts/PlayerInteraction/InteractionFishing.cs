using UnityEngine;

public class InteractionFishing : MonoBehaviour
{
    bool collide = false;


    void Update()
    {
        if(collide && (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Slash))){
            //scene switch happens here
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
