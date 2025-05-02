using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlipper : MonoBehaviour
{
    public Light2D[] levelLights;
    

    // Update is called once per frame
    void Update()
    {
        if (LeverTracker.leversSwitched[PlayerRoomTracker.playerCurrentRoomIndex] == true){
            levelLights[PlayerRoomTracker.playerCurrentRoomIndex].intensity = 1;
        }
    }
}
