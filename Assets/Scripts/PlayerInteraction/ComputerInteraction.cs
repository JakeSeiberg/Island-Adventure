using UnityEngine;
using UnityEngine.SceneManagement;

public class ComputerInteraction : MonoBehaviour
{
    private bool playerNearby = false;
    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E Pressed");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            playerData.playerPosition = PlayerMovement.currentPlayerPos;
            playerData.playerRotation = PlayerCamera.currentRotation;
            toolTips.changeScene();
            playerData.curScene = "CityBikerMenu";
            SceneManager.LoadScene("CityBikerMenu");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Press E to play Frogger!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
