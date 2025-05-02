using UnityEngine;
using System.Collections;

public class DoorBackColliderScript : MonoBehaviour
{

    public GameObject player;
    public int roomIndexAfterTeleport;

    private bool playerInRange = false;

    private Vector2 teleportOffset;
    public GameObject door;
    
    void Start()
    {
        teleportOffset.x = -4.20183f;
        teleportOffset.y = 2.95f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)))
        {
            StartCoroutine(teleportDoor());
            _AudioManager.Instance.playDoorSound();
        }

    }

    IEnumerator teleportDoor()
    {   
        yield return new WaitForSeconds(.5f); 

        
        Vector3 newPosition = door.transform.position;
        newPosition.x += teleportOffset.x; 
        newPosition.y += teleportOffset.y; 
        player.transform.localPosition = newPosition; 
        PlayerRoomTracker.SetPlayerRoomIndex(roomIndexAfterTeleport);
    }

}
