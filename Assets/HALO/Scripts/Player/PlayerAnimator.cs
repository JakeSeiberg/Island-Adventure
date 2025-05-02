using UnityEngine;

public class _PlayerAnimator : MonoBehaviour
{
    private Animator playerAnimator;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        checkWalk();
    }

    void checkWalk(){
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            playerAnimator.SetBool("IsAPress", true);
        }
        else{
            playerAnimator.SetBool("IsAPress", false);
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            playerAnimator.SetBool("IsDPress", true);
        }
        else{
            playerAnimator.SetBool("IsDPress", false);
        }

        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            playerAnimator.SetBool("IsSPress", true);
        }
        else{
            playerAnimator.SetBool("IsSPress", false);
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            playerAnimator.SetBool("IsWPress", true);
        }
        else{
            playerAnimator.SetBool("IsWPress", false);
        }
        
    }
}
