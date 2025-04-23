using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxMaterials;
    public Vector3[] sunPositions;

    public GameObject sun;

    private int curSkybox = 0;
    public bool skyCooldown = true;
    
    void Start()
    {
        RenderSettings.skybox = skyboxMaterials[curSkybox];
        }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Update()
    {
        /*
        if(Input.GetKey(KeyCode.G) && skyCooldown){
            ChangeSkybox(curSkybox);

        }
        if(Input.GetKey(KeyCode.H)){
            sun.transform.eulerAngles = sunPositions[curSkybox];
            skyCooldown = true;
        }*/
    }

    public void ChangeSkybox(){
        RenderSettings.skybox = skyboxMaterials[curSkybox];
        sun.transform.eulerAngles = sunPositions[curSkybox];
        skyCooldown = false;
        curSkybox += 1;
        if (curSkybox >= 5){
            curSkybox = 0;
        }
        
    }
}
