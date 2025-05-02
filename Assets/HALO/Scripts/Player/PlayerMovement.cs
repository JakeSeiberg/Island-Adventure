using UnityEngine;

public class _PlayerMovement : MonoBehaviour
{
    public float speed;
    private bool checkSfx;

    void Start()
    {
        checkSfx = false;
    }

    void Update()
    {
        handleMovement(); 
        if(checkSfx){
            playFootsteps();
            checkSfx = false;
        }
    }

    void handleMovement(){
        Vector3 pos = transform.position;
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            pos.x += speed * Time.deltaTime;
            checkSfx = true;
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            pos.x -= speed * Time.deltaTime;
            checkSfx = true;
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            pos.y += speed * Time.deltaTime;
            checkSfx = true;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            pos.y -= speed * Time.deltaTime;
            checkSfx = true;
        }  
        
        transform.position = pos;
    }

    void playFootsteps(){
         _AudioManager.Instance.playFootstepSound();
    }

}
