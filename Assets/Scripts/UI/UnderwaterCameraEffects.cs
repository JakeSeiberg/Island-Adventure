using UnityEngine;
using UnityEngine.Rendering;

public class UnderwaterCameraEffects : MonoBehaviour
{
   [SerializeField] private Transform mainCamera;
    [SerializeField] private int depth = 0;

    [SerializeField] private Volume postProcessingVolume;

    [SerializeField] private VolumeProfile surfacePostProcessing;
    [SerializeField] private VolumeProfile underwaterPostProcessing;

    private void Update()
    {
        if (mainCamera.position.y < depth){
            EnableEffects(true);
            
        }
        else{
            EnableEffects(false);
        }

    }

    private void EnableEffects(bool active){
        if (active){
            postProcessingVolume.profile = underwaterPostProcessing;
            RenderSettings.fog = true;
        }
        else{
            postProcessingVolume.profile = surfacePostProcessing;
            RenderSettings.fog = false;
        }
    }
}
