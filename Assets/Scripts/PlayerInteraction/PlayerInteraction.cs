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

    public FishCookerScript fishCookerScript;

    public FireTimerUIScript fireTimerScript;

    public BedScript bedScript;

    public GameObject boatGas;
    public GameObject boatMotor;
    public GameObject boatSail;

    public AudioManager audioManager;
    
    private RaycastHit hit; 

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


        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {

            if (input()){
                
                if (hit.collider.CompareTag("Worm")) 
                {
                    clickWorm();
                }
                if (hit.collider.CompareTag("spearItem")) 
                {
                    clickSpear();
                }
                if (hit.collider.CompareTag("axeItem")) 
                {
                    clickAxe();
                }
                if (hit.collider.CompareTag("SpearInteractable"))
                {
                    clickSpearFishingSpot();
                }
                if (hit.collider.CompareTag("Tree")) 
                {
                    clickTree();
                }
                if (hit.collider.CompareTag("Leaf")) 
                {
                    clickLeaf();
                }
                if (hit.collider.CompareTag("Log")) 
                {
                    clickLog();
                }
                
                if (hit.collider.CompareTag("Campfire")) 
                {
                    clickCampfire();
                }

                if (hit.collider.CompareTag("CampfireGrate"))
                {
                    clickCampfireGrate();
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
                    clickWorkstation();
                }
                if (hit.collider.CompareTag("BoatSail"))
                {
                    clickBoatSail();
                }
                if (hit.collider.CompareTag("BoatMotor"))
                {
                    clickBoatMotor();
                }
                if (hit.collider.CompareTag("BoatGas"))
                {
                    clickBoatGas();
                }
                if (hit.collider.CompareTag("GetawayBoat"))
                {
                    clickGetawayBoat();
                }
                if (hit.collider.CompareTag("Bed"))
                {
                    Debug.Log("Sleeping");
                    bedScript.interact();
                }
                if (hit.collider.CompareTag("CityBiker"))
                {
                    clickCityBiker();
                }
            }
            else{
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

                Outline outline = hit.collider.GetComponent<Outline>();
                if (outline != null)
                {
                    if (currentOutline != outline)
                    {
                        if (currentOutline != null)
                        {
                            currentOutline.enabled = false; 
                        }
                        currentOutline = outline; 
                        currentOutline.enabled = true; 

                        enableFireTimer();
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

    private void clickWorm()
    {
        if (!playerData.canSleep)
        {
            audioManager.playPickup();
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

    private void clickSpear()
    {
        audioManager.playPickup();
        spearPickupScript spear = hit.collider.GetComponent<spearPickupScript>();
        if (spear != null)
        {
            playerData.hasSpear = true;
            spear.hasSpear();
        }
    }

    private void clickAxe()
    {
        audioManager.playPickup();
        axePickupScript axe = hit.collider.GetComponent<axePickupScript>();
        axe.hasAxe();
    }

    private void clickSpearFishingSpot()
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

    private void clickTree()
    {
        if (playerData.hasAxe)
        {
            if (!playerData.canSleep)
            {
                audioManager.playPickup();
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
                playerData.sleepScore += 15;
                SceneManager.LoadScene("Tree");
            }
            else
            {
                toolTips.tip("You're starting to get tired, Maybe you should get some rest", 5f);
            }
        }
    }

    private void clickLeaf()
    {
        audioManager.playPickup();
        LeafFallingScript leaf = hit.collider.GetComponent<LeafFallingScript>();
        leaf.leafCollected();
    }

    private void clickLog()
    {
        audioManager.playPickup();
        logScript log = hit.collider.GetComponent<logScript>();
        log.logCollected();
    }

    private void clickCampfire()
    {
        CampfireScript.interact();
        playerData.hasBurnedWood = true;
    }

    private void clickCampfireGrate()
    {
        Debug.Log("Grate interacted");
        fishCookerScript.interact();
        playerData.hasPlacedFish = true;
    }

    private void clickWorkstation()
    {
        audioManager.playPickup();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playerData.playerPosition = PlayerMovement.currentPlayerPos;
        playerData.playerRotation = PlayerCamera.currentRotation;
        playerData.hasOpenedShop = true;
        toolTips.changeScene();
        playerData.curScene = "Shop";
        SceneManager.LoadScene("Shop");
    }

    private void clickBoatSail()
    {
        audioManager.playPickup();
        playerData.boatSail = true;
        boatSail.SetActive(false);
    }

    private void clickBoatMotor()
    {
        audioManager.playPickup();
        playerData.boatMotor = true;
        boatMotor.SetActive(false);
    }

    private void clickBoatGas()
    {
        audioManager.playPickup();
        playerData.boatGas = true;
        boatGas.SetActive(false);
    }

    private void clickGetawayBoat()
    {
        Debug.Log("Boat Hit");
        if (playerData.boatHull && playerData.boatSail && playerData.boatMotor && playerData.boatGas)
        {
            playerData.hasEscaped = true;
            Debug.Log("You've Escaped!");
            toolTips.tip("You escaped!", 5f);

            toolTips.changeScene();
            playerData.curScene = "WinScene";
            playerData.sleepScore = 100;
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            Debug.Log("Player Has Not Escaped!");
            toolTips.tip("You need to find more parts!", 5f);
        }
    }

    private void clickCityBiker()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerData.playerPosition = PlayerMovement.currentPlayerPos;
        playerData.playerRotation = PlayerCamera.currentRotation;
        toolTips.changeScene();
        playerData.curScene = "CityBikerMenu";
        SceneManager.LoadScene("CityBikerMenu");
    }

    private void enableFireTimer()
    {
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