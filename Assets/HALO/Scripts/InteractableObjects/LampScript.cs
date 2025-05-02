using UnityEngine;

public class LampScript : MonoBehaviour
{
    public SpriteRenderer LampRenderer; 
    public Sprite lampLit;
    public int roomIndex;
    
    void Update()
    {
        if (LeverTracker.leversSwitched[roomIndex] == true)
        {
            LampRenderer.sprite = lampLit;
        }
    }
}
