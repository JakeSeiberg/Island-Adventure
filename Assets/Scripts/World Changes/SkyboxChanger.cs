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

        playerData.curSkybox ++;
        if (playerData.curSkybox >= skyboxMaterials.Length){
            playerData.curSkybox = 0;
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
