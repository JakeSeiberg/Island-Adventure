
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Interaction : MonoBehaviour
{
    public Light2D lights;
    private bool maxLight = false;

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space) && LightTime.maxTime > 0){
            LightUp();
            if (lights.pointLightOuterRadius >= 2.7){
                maxLight = true;
            }
            else if (lights.pointLightOuterRadius <= 2.6){
                maxLight = false;
            }
            LightTime.LightConsumed();
        }
        else if (lights.pointLightOuterRadius > 1.6){
            LightDown();
        }
        if (_BedScript.AreAllTrue(LeverTracker.leversSwitched)){
            lights.intensity = 0;
        }
    }

    void LightUp(){
        if (maxLight){
            lights.pointLightInnerRadius += 0;
            lights.pointLightOuterRadius += 0;
        }
        else{
            lights.pointLightInnerRadius += 0.06f;
            lights.pointLightOuterRadius += 0.06f;
        }

    }
    void LightDown(){
        lights.pointLightInnerRadius -= 0.006f;
        lights.pointLightOuterRadius -= 0.006f;
    }
}
