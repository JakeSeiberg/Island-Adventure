using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Drowning : MonoBehaviour
{
    private bool isDrowning = false;
    private float drowningYLevel = -2.8f;

    public GameObject airBarFull;
    public GameObject airBarEmpty;

    void Start()
    {
        airBarFull.SetActive(false);
        airBarEmpty.SetActive(false);
    }


    void FixedUpdate()
    {
        if (PlayerMovement.currentPlayerPos.y < drowningYLevel)
        {
            if (!isDrowning)
            {
                StartCoroutine(IsDrowning());
                isDrowning = true;
            }
        }
        else
        {
            isDrowning = false;
            playerData.air = 100f; 
        }
    }

    private IEnumerator IsDrowning()
    {
        while (PlayerMovement.currentPlayerPos.y < drowningYLevel)
        {
            airBarFull.GetComponent<UnityEngine.UI.Image>().fillAmount = playerData.air / 100f;
            airBarFull.SetActive(true);
            airBarEmpty.SetActive(true);
            yield return new WaitForSeconds(0.15f);
            playerData.air -= 1f;

            if (playerData.air <= 0)
            {
                Debug.Log("Player has died due to drowning.");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                toolTips.changeScene();
                playerData.curScene = "DeathScene";
                SceneManager.LoadScene("DeathScene");
                yield break; 
            }
        }

        playerData.air = 100f;
        isDrowning = false;
        airBarFull.SetActive(false);
        airBarEmpty.SetActive(false);


    }
}
