using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxMaterials;
    public Vector3[] sunPositions;

    public GameObject sun;
    
    void Start()
    {
        //if sleepScore between 0 and 20, set skybox to 0
        //if sleepScore between 20 and 40, set skybox to 1
        //if sleepScore between 40 and 60, set skybox to 2
        //if sleepScore between 60 and 80, set skybox to 3
        //if sleepScore between 80 and 100, set skybox to 4
        Debug.Log("B4: " + playerData.sleepScore + " " + playerData.curSkybox);

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

        if (playerData.curSkybox == 4 && (playerData.woodCount < 10 || playerData.woodCount < 5))
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

        Debug.Log("After: " + playerData.sleepScore + " " + playerData.curSkybox);

        


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
