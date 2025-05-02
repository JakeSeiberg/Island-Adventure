using UnityEngine;

public class DisplayShift : MonoBehaviour
{
    public static bool[] shiftColliders = {false, false, false, false};
    public float offset = -1.2f; 

    public GameObject player;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if (shiftColliders[0] && LeverTracker.leversSwitched[PlayerRoomTracker.playerCurrentRoomIndex]) {
            showShift();
        }
        else if (shiftColliders[1]){
            showShift();
        }
        else if (shiftColliders[2]){
            showShift();
        }
        else if (shiftColliders[3]){
            showShift();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void showShift()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Vector3 playerPosition = player.transform.position;
        Vector3 spacebarPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        transform.position = spacebarPosition;
    }

    public static void resetBools(){
        for (int i = 0; i < shiftColliders.Length; i++)
        {
            shiftColliders[i] = false;
        }
    }
}
