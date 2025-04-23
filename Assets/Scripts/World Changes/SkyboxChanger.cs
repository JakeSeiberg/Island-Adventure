using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxMaterials;
    public Vector3[] sunPositions;

    public GameObject sun;
    private static int curSkybox = 0;
    public bool skyCooldown = true;
    
    void Start()
    {
        RenderSettings.skybox = skyboxMaterials[curSkybox];
        DynamicGI.UpdateEnvironment();
        sun.transform.eulerAngles = sunPositions[curSkybox];
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeSkybox(){
        RenderSettings.skybox = skyboxMaterials[curSkybox];
        DynamicGI.UpdateEnvironment();
        sun.transform.eulerAngles = sunPositions[curSkybox];
        skyCooldown = false;
        curSkybox ++;
        if (curSkybox >= skyboxMaterials.Length){
            curSkybox = 0;
        }
        
    }
}
