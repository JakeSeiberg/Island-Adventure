using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    private float rayDistance = 3.3f;
    public Transform playerPosition;
    public Transform playerCamera;

    public LayerMask interactableLayer;

    private float rayHeightOffset = 3.7f;

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
        Vector3 tempPosition = playerPosition.position;
        tempPosition.y += rayHeightOffset;
        Ray ray = new Ray(tempPosition, playerCamera.forward);
        RaycastHit hit;

        Debug.DrawRay(tempPosition, playerCamera.forward * rayDistance, Color.red, 1f);


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
            if (hit.collider.CompareTag("axeItem")) 
            {
                axePickupScript axe = hit.collider.GetComponent<axePickupScript>();
                axe.hasAxe();
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
                if (playerData.hasAxe)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    playerData.playerPosition = PlayerMovement.currentPlayerPos;
                    playerData.playerRotation = PlayerCamera.currentRotation;
                    playerData.hasEnteredTreeGame = true;

                    playerData.treeChopped = false;
                    TreeID treeIDScript = hit.collider.GetComponent<TreeID>();
                    if (treeIDScript != null)
                    {
                        playerData.currentTreeID = treeIDScript.treeID;
                        Debug.Log("Saved tree ID: " + playerData.currentTreeID);
                    }

                    toolTips.changeScene();
                    playerData.curScene = "Tree";
                    if (!playerData.hasPlayedTreeGame)
                    {
                        toolTips.delayedToolTip("Press Spacebar while the white bar is in the green area to chop the tree",5f);
                        playerData.hasPlayedTreeGame = true;
                    }
                    SceneManager.LoadScene("Tree");
                }
            }
        }
    }
}