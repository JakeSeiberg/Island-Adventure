using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxMaterials;

    private int curSkybox = 2;
    public bool skyCooldown = true;
    
    void Start()
    {
        RenderSettings.skybox = skyboxMaterials[curSkybox];
    }

    
    void Update()
    {
        if(Input.GetKey(KeyCode.G) && skyCooldown){
            ChangeSkybox(curSkybox);
            skyCooldown = false;
        }
        if(Input.GetKey(KeyCode.H)){
            skyCooldown = true;
        }
    }

    void ChangeSkybox(int newSkybox){
        RenderSettings.skybox = skyboxMaterials[newSkybox];
        curSkybox += 1;
        if (curSkybox >= 10){
            curSkybox = 0;
        }
    }
}
