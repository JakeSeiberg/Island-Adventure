using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    private float rayDistance = 3.3f;
    public Transform playerPosition;
    public Transform playerCamera;
    public Transform orientation;

    public LayerMask interactableLayer;

    private float rayHeightOffset = 3.7f;

    private Outline currentOutline;

    public CampfireScript CampfireScript;

    public fishCookerScript fishCookerScript;

    public fireTimerUIScript fireTimerScript;

    public BedScript bedScript;

    public GameObject boatGas;
    public GameObject boatMotor;
    public GameObject boatSail;

    void Start()
    {
        manageBoatItemsPickup();
        playerData.curScene = "MainWorld";
    }

    void Update()
    {
        interact();
    }

    void FixedUpdate()
    {
        manageBoatItemsPickup();
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
        tempPosition += orientation.forward * 0.25f;
        Ray ray = new Ray(tempPosition, playerCamera.forward);
        RaycastHit hit;

//        Debug.DrawRay(tempPosition, playerCamera.forward * rayDistance, Color.red, 1f);


        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {

            if (input()){
                if (hit.collider.CompareTag("Worm")) 
                {
                    if (!playerData.canSleep)
                    {
                        wormInteract wormScript = hit.collider.GetComponent<wormInteract>();
                        if (wormScript != null)
                        {
                            playerData.hasPickedUpAWorm = true;
                            wormScript.wormCollected();
                        }
                        playerData.sleepScore += 2;
                    }
                    else{
                        toolTips.tip("You're starting to get tired, Maybe you should get some rest", 5f);
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
                if (hit.collider.CompareTag("SpearInteractable"))// && !playerData.canSleep) //playerData.curSkybox != 4)
                {
                    if (!playerData.canSleep)
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
                    else{
                        toolTips.tip("You're starting to get tired, Maybe you should get some rest", 5f);
                    }
                }
                if (hit.collider.CompareTag("Tree")) 
                {
                    if (playerData.hasAxe)// && !playerData.canSleep)
                    {
                        if (!playerData.canSleep)
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
                            playerData.sleepScore += 15;
                            SceneManager.LoadScene("Tree");
                        }
                        else
                        {
                            toolTips.tip("You're starting to get tired, Maybe you should get some rest", 5f);
                        }
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
                    playerData.hasBurnedWood = true;
                }

                if (hit.collider.CompareTag("CampfireGrate"))
                {
                    Debug.Log("Grate interacted");
                    fishCookerScript.interact();
                    playerData.hasPlacedFish = true;
                }
                if (hit.collider.CompareTag("fishLeft"))
                {
                    fishCookerScript.fishLeft();
                }
                if (hit.collider.CompareTag("fishRight"))
                {
                    fishCookerScript.fishRight();
                }
                if (hit.collider.CompareTag("workStation"))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    playerData.playerPosition = PlayerMovement.currentPlayerPos;
                    playerData.playerRotation = PlayerCamera.currentRotation;
                    playerData.hasOpenedShop = true;
                    toolTips.changeScene();
                    playerData.curScene = "Shop";
                    SceneManager.LoadScene("Shop");
                }
                if (hit.collider.CompareTag("BoatSail"))
                {
                    playerData.boatSail = true;
                    boatSail.SetActive(false);
                }
                if (hit.collider.CompareTag("BoatMotor"))
                {
                    playerData.boatMotor = true;
                    boatMotor.SetActive(false);
                }
                if (hit.collider.CompareTag("BoatGas"))
                {
                    playerData.boatGas = true;
                    boatGas.SetActive(false);
                }
                if (hit.collider.CompareTag("GetawayBoat"))
                {
                    Debug.Log("Boat Hit");
                    if (playerData.boatHull && playerData.boatSail && playerData.boatMotor && playerData.boatGas)
                    {
                        playerData.hasEscaped = true;
                        Debug.Log("You've Escaped!");
                        toolTips.tip("You escaped!", 5f);
                    }
                    else
                    {
                        Debug.Log("Player Has Not Escaped!");
                        toolTips.tip("You need to find more parts!", 5f);
                    }
                }
                if (hit.collider.CompareTag("Bed"))
                {
                    Debug.Log("Sleeping");
                    bedScript.interact();
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

                        if (currentOutline.gameObject.CompareTag("Campfire") || currentOutline.gameObject.CompareTag("fishLeft") || currentOutline.gameObject.CompareTag("fishRight") || currentOutline.gameObject.CompareTag("CampfireGrate"))
                        {
                            fireTimerScript.isActive = true;
                        }
                        else
                        {
                            fireTimerScript.isActive = false;
                        }
                    }
                }
                else
                {
                    // If no outline component is found, disable the current outline
                    if (currentOutline != null)
                    {
                        currentOutline.enabled = false;
                        currentOutline = null;
                        fireTimerScript.isActive = false;
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
                fireTimerScript.isActive = false;
            }
        }
    }

    private void manageBoatItemsPickup()
    {
        if (playerData.boatSail)
        {
            boatSail.SetActive(false);
        }
        else
        {
            boatSail.SetActive(true);
        }
        if (playerData.boatMotor)
        {
            boatMotor.SetActive(false);
        }
        else
        {
            boatMotor.SetActive(true);
        }
        if (playerData.boatGas)
        {
            boatGas.SetActive(false);
        }
        else
        {
            boatGas.SetActive(true);
        }
    }
}