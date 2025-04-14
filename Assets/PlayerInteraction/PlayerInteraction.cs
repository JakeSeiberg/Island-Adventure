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

    private Outline currentOutline;

    public CampfireScript CampfireScript;

    public fishCookerScript fishCookerScript;

    void Start()
    {

    }

    void Update()
    {
        interact();
    }

    private bool input()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void interact()
    {
        Vector3 tempPosition = playerPosition.position;
        tempPosition.y += rayHeightOffset;
        Ray ray = new Ray(tempPosition, playerCamera.forward);
        RaycastHit hit;

//        Debug.DrawRay(tempPosition, playerCamera.forward * rayDistance, Color.red, 1f);


        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            if (input()){
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
                if (hit.collider.CompareTag("Leaf")) 
                {
                    LeafFallingScript leaf = hit.collider.GetComponent<LeafFallingScript>();
                    leaf.leafCollected();
                }
                if (hit.collider.CompareTag("Log")) 
                {
                    logScript log = hit.collider.GetComponent<logScript>();
                    log.logCollected();
                }
                if (hit.collider.CompareTag("Campfire")) 
                {
                    CampfireScript.interact();
                }
                if (hit.collider.CompareTag("CampfireGrate"))
                {
                    Debug.Log("Grate interacted");
                    fishCookerScript.interact();
                }
                if (hit.collider.CompareTag("fishLeft"))
                {
                    fishCookerScript.fishLeft();
                }
                if (hit.collider.CompareTag("fishRight"))
                {
                    fishCookerScript.fishRight();
                }
            }
            else{
                // Handle outline logic
                if (hit.collider.CompareTag("Tree"))
                {
                    if (!playerData.hasAxe)
                    {
                        return;
                    }
                }
                if (hit.collider.CompareTag("SpearInteractable"))
                {
                    if (!playerData.hasSpear)
                    {
                        return;
                    }
                }

                    // Check if the object has an Outline component
                Outline outline = hit.collider.GetComponent<Outline>();
                if (outline != null)
                {
                    // Enable the outline on the currently hit object
                    if (currentOutline != outline)
                    {
                        if (currentOutline != null)
                        {
                            currentOutline.enabled = false; // Disable the previous outline
                        }
                        currentOutline = outline; // Update the current outline
                        currentOutline.enabled = true; // Enable the new outline
                    }
                }
                else
                {
                    // If no outline component is found, disable the current outline
                    if (currentOutline != null)
                    {
                        currentOutline.enabled = false;
                        currentOutline = null;
                    }
                }
            }
        }
        else
        {
            if (currentOutline != null)
            {
                currentOutline.enabled = false;
                currentOutline = null;
            }
        }
    }
}