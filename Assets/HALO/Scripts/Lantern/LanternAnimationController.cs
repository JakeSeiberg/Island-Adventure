using UnityEngine;

public class LanternAnimationController : MonoBehaviour
{
    private Animator lanternAnimator;
    private bool lanternOnCheck;
    void Start()
    {
        lanternAnimator = GetComponent<Animator>();
        lanternOnCheck = false;
    }
    
    void FixedUpdate()
    {
        if (!_BedScript.AreAllTrue(LeverTracker.leversSwitched))
        {
            checkLanternStatus();
        }
    }

    void checkLanternStatus(){
        if(Input.GetKey(KeyCode.Space)){
            lanternOnCheck = true;
            lanternAnimator.SetBool("IsSpacePress", true);
        }
        else{
            lanternOnCheck = false;
            lanternAnimator.SetBool("IsSpacePress", false);
        }

        if(lanternOnCheck && !Input.GetKey(KeyCode.Space)){

            lanternOnCheck = false;
            lanternAnimator.SetBool("IsSpacePress", false);
        }   
    }

}
