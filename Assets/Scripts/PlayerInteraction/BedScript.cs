using UnityEngine;
using UnityEngine.SceneManagement;

public class BedScript : MonoBehaviour
{
    public void interact()
    {
        if (playerData.canSleep)
        {
            playerData.playerPosition = PlayerMovement.currentPlayerPos;
            playerData.playerRotation = PlayerCamera.currentRotation;
            playerData.hasSlept = true;
            toolTips.changeScene();
            playerData.curScene = "SleepScene";
            SceneManager.LoadScene("SleepScene");
        }
        else
        {
            toolTips.tip("It's not night time yet",5);
        }
    }
}
