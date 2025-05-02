using UnityEngine;

public class BedDreamTooltipScript : MonoBehaviour
{
    private float offsetX = 1.5f; 
    private float offsetY = 1.7f; 

    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    void FixedUpdate()
    {
        if (_BedScript.AreAllTrue(LeverTracker.leversSwitched))
        {
            showDream();
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
        

    void showDream()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Vector3 playerPosition = player.transform.position;
        Vector3 dreamPosition = new Vector3(playerPosition.x + offsetX, playerPosition.y + offsetY, playerPosition.z);
        transform.position = dreamPosition;
    }
}
