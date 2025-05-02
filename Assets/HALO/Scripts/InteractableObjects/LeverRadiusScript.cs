using UnityEngine;
using System.Collections;

public class LeverRadiusScript : MonoBehaviour
{
    public bool[] leversSwitched = {false, false};

    public int leverIndex;

    public SpriteRenderer leverSpriteRenderer; 
    public Sprite[] leverSprites; 
    public float frameDelay = 0.08f; 

    private bool playerInRange = false;
    private bool isAnimating = false;
    
    private bool hasSwitched = false;

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
        if (playerInRange && (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)) && !isAnimating && !hasSwitched)
        {
            StartCoroutine(PlayLeverAnimation());
            _AudioManager.Instance.playLightSound();
        }

    }

    IEnumerator PlayLeverAnimation()
    {
        isAnimating = true;
        hasSwitched = true;
        LeverTracker.leversSwitched[leverIndex] = true;

        for (int i = 0; i < leverSprites.Length; i++)
        {
            leverSpriteRenderer.sprite = leverSprites[i];
            yield return new WaitForSeconds(frameDelay); 
        }

        isAnimating = false;
    }

}
