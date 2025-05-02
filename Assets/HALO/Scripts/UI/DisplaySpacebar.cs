using UnityEngine;

public class DisplaySpacebar : MonoBehaviour
{
    public GameObject player;
    public float offset = -5f; 

    void Update()
    {
        if (player != null && PlayerRoomTracker.playerCurrentRoomIndex == 0 && !_BedScript.AreAllTrue(LeverTracker.leversSwitched) && LeverTracker.leversSwitched[0] == false) 
        {
            if (Input.GetKey(KeyCode.Space))
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                Vector3 playerPosition = player.transform.position;
                Vector3 spacebarPosition = new Vector3(playerPosition.x, playerPosition.y + offset, playerPosition.z);
                transform.position = spacebarPosition;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
