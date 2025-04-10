using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    private float rayDistance = 3.3f;
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
        }
    }

    private void interact()
    {
        Ray ray = new Ray(playerPosition.position, playerCamera.forward);
        RaycastHit hit;

        Debug.DrawRay(playerPosition.position, playerCamera.forward * rayDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            if (hit.collider.CompareTag("Worm")) 
            {

                wormInteract wormScript = hit.collider.GetComponent<wormInteract>();
                if (wormScript != null)
                {
                    playerData.hasPickedUpAWorm = true;
                    wormScript.wormCollected();
                }
            }
            if (hit.collider.CompareTag("spearItem")) 
            {
                spearPickupScript spear = hit.collider.GetComponent<spearPickupScript>();
                if (spear != null)
                {
                    playerData.hasSpear = true;
                    spear.hasSpear();
                }
            }
            if (hit.collider.CompareTag("SpearInteractable")) 
            {
                playerData.hasGoneFishing = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                playerData.playerPosition = PlayerMovement.currentPlayerPos;
                playerData.playerRotation = PlayerCamera.currentRotation;
                toolTips.changeScene();
                playerData.curScene = "Fishing";
                SceneManager.LoadScene("Fishing");
            }
            if (hit.collider.CompareTag("Tree")) 
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                playerData.playerPosition = PlayerMovement.currentPlayerPos;
                playerData.playerRotation = PlayerCamera.currentRotation;
                playerData.hasBrokenTree = true;
                toolTips.changeScene();
                playerData.curScene = "Tree";
                SceneManager.LoadScene("Tree");
            }
        }
    }
}