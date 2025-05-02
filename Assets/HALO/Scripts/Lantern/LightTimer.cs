using UnityEngine;


public class LightTime : MonoBehaviour
{
    public static float maxTime = 14f;
    private SpriteRenderer _lightSpriteRenderer;
    public Sprite[] timerSprites;
    void Start()
    {
        _lightSpriteRenderer = GetComponent<SpriteRenderer>();
        maxTime = 14f;
    }

    void Update()
    {
        if (!_BedScript.AreAllTrue(LeverTracker.leversSwitched))
        {
            if (maxTime > 12 && maxTime < 14){
                _lightSpriteRenderer.sprite = timerSprites[0];
            }
            if (maxTime > 10 && maxTime <= 12){
                _lightSpriteRenderer.sprite = timerSprites[1];
            }
            if (maxTime > 8 && maxTime <= 10){
                _lightSpriteRenderer.sprite = timerSprites[2];
            }
            if (maxTime > 6 && maxTime <= 8){
                _lightSpriteRenderer.sprite = timerSprites[3];
            }
            if (maxTime > 4 && maxTime <= 6){
                _lightSpriteRenderer.sprite = timerSprites[4];
            }
            if (maxTime > 3 && maxTime <= 4){
                _lightSpriteRenderer.sprite = timerSprites[5];
            }
            if (maxTime > 2 && maxTime <= 3){
                _lightSpriteRenderer.sprite = timerSprites[6];
            }
            if (maxTime > 0 && maxTime <= 2){
                _lightSpriteRenderer.sprite = timerSprites[6];
            }
            if (maxTime <= 0){
                _lightSpriteRenderer.enabled = false;
            }
        }
        else
        {
            _lightSpriteRenderer.enabled = false;
        }

    }
    public static void LightConsumed(){
        maxTime -= Time.deltaTime;
        
    }
}
