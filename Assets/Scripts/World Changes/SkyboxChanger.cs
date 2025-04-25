using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxMaterials;
    public Vector3[] sunPositions;

    public GameObject sun;
    
    void Start()
    {

        if (playerData.sleepScore < 25)
        {
            playerData.curSkybox = 0;
        }
        else if (playerData.sleepScore >= 25 && playerData.sleepScore < 50)
        {
            playerData.curSkybox = 1;
        }
        else if (playerData.sleepScore >= 50 && playerData.sleepScore < 75)
        {
            playerData.curSkybox = 2;
        }
        else if (playerData.sleepScore >= 75 && playerData.sleepScore < 100)
        {
            playerData.curSkybox = 3;
        }
        else if (playerData.sleepScore >= 100)
        {
            playerData.curSkybox = 4;
        }

        if (playerData.curSkybox == 4 && (playerData.woodCount < 10 || playerData.leafCount < 5) && !playerData.hasBoughtBed)
        {
            playerData.curSkybox = 3;
        }

        if (playerData.curSkybox == 4)
        {
            playerData.canSleep = true;
        }
        else
        {
            playerData.canSleep = false;
        }

        RenderSettings.skybox = skyboxMaterials[playerData.curSkybox];
        DynamicGI.UpdateEnvironment();
        sun.transform.eulerAngles = sunPositions[playerData.curSkybox];

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
