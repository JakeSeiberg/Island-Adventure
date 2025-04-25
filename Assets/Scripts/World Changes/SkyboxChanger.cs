using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxMaterials;
    public Vector3[] sunPositions;

    public GameObject sun;
    
    void Start()
    {
        RenderSettings.skybox = skyboxMaterials[playerData.curSkybox];
        DynamicGI.UpdateEnvironment();
        sun.transform.eulerAngles = sunPositions[playerData.curSkybox];

        if (playerData.actionsTaken % 1 != 0){
            Mathf.Floor(playerData.actionsTaken);
        }
        if (playerData.actionsTaken % 3 < 0.01f && playerData.actionsTaken != 0){
            playerData.curSkybox ++;
        }

        if (playerData.curSkybox >= skyboxMaterials.Length){
            playerData.curSkybox = 0;
        }

        if (playerData.curSkybox == 4)
        {
            playerData.canSleep = true;
        }
        else
        {
            playerData.canSleep = false;
        }
    }

/*
    public void ChangeSkybox(){
        RenderSettings.skybox = skyboxMaterials[playerData.curSkybox];
        DynamicGI.UpdateEnvironment();
        sun.transform.eulerAngles = sunPositions[playerData.curSkybox];


        playerData.curSkybox ++;
        if (playerData.curSkybox >= skyboxMaterials.Length){
            playerData.curSkybox = 0;
        }
        
    }*/
}
