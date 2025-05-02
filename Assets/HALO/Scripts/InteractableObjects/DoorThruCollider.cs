using UnityEngine;
using System.Collections;

public class DoorThruCollider : MonoBehaviour
{

    public SpriteRenderer doorSpriteRenderer;
    public Sprite[] doorSprites; 
    private float frameDelay = 0.12f; 
    public int roomIndexAfterTeleport;

    private Vector2 teleportOffset;
    public GameObject door;

    public GameObject player;

    private bool playerInRange = false;
    
    private bool hasOpened = false;

    void Start()
    {
        teleportOffset.x = -4.20183f;
        teleportOffset.y = 6.33f;
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
        if (playerInRange && (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)) && LeverTracker.leversSwitched[PlayerRoomTracker.playerCurrentRoomIndex] == true)
        {
            StartCoroutine(PlayDoorAnimation());
            _AudioManager.Instance.playDoorSound();
        }

    }
    
    IEnumerator PlayDoorAnimation()
    {
        
        if (!hasOpened){
            hasOpened = true;
            for (int i = 0; i < doorSprites.Length; i++)
            {
                doorSpriteRenderer.sprite = doorSprites[i]; 
                yield return new WaitForSeconds(frameDelay); 
            }

        }
        
        yield return new WaitForSeconds(.5f);

        
        Vector3 newPosition = door.transform.position;
        newPosition.x += teleportOffset.x; 
        newPosition.y += teleportOffset.y;  
        player.transform.localPosition = newPosition; 
        PlayerRoomTracker.SetPlayerRoomIndex(roomIndexAfterTeleport);

    }

}
