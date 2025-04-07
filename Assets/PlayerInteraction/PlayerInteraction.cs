using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public float rayDistance = 3f;
    public Transform playerPosition; 
    public Transform playerCamera;

    public LayerMask interactableLayer;
    

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            interact();
            Debug.Log("e hit");
        }
    }

    private void interact()
    {
        Ray ray = new Ray(playerPosition.position, playerCamera.forward);
        RaycastHit hit;

        Debug.DrawRay(playerPosition.position, playerCamera.forward * rayDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (hit.collider.CompareTag("Worm")) 
            {
                wormInteract.wormCollected(); 
                Debug.Log("Worm collected!");
            }
            if (hit.collider.CompareTag("SpearInteractable")) 
            {
                Debug.Log("Spear clicked!");
                SceneManager.LoadScene("FishingMinigame", LoadSceneMode.Additive);
            }
        }
    }

}