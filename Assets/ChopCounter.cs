using TMPro;
using UnityEngine;
using System.Collections;

public class ChopCounter : MonoBehaviour
{
    public TextMeshPro counterText;
    public SpriteRenderer checkmarkSprite;
    public SpriteRenderer xmarkSprite;

    public GameObject bambooFrame;
    public GameObject completionText;
    public GameObject blackPlane;

    public AxeScript axeController;

    public int hits = 0;
    public int hitsNeeded = 5;

    void Start()
    {
        checkmarkSprite.enabled = false;
        xmarkSprite.enabled = false;
        counterText.text = hits + "/" + hitsNeeded;

        // ✅ Hide end-game UI at start
        bambooFrame.SetActive(false);
        completionText.SetActive(false);
        blackPlane.SetActive(false);
    }

    public void RegisterHit()
    {
        hits++;

        if (hits > hitsNeeded)
            hits = hitsNeeded;

        counterText.text = hits + "/" + hitsNeeded;

        StartCoroutine(FlashCheckmark());

        if (hits == hitsNeeded)
        {
            Debug.Log("Tree chopped!");
            playerData.woodCount++;

            // ✅ Show the UI elements
            bambooFrame.SetActive(true);
            completionText.SetActive(true);
            blackPlane.SetActive(true);

            // ✅ Disable the axe controller
            if (axeController != null)
                axeController.enabled = false;
        }
    }

    public void RegisterMiss()
    {
        StartCoroutine(FlashXMark());
    }

    IEnumerator FlashCheckmark()
    {
        checkmarkSprite.enabled = true;
        yield return new WaitForSeconds(0.4f);
        checkmarkSprite.enabled = false;
    }

    IEnumerator FlashXMark()
    {
        xmarkSprite.enabled = true;
        yield return new WaitForSeconds(0.4f);
        xmarkSprite.enabled = false;
    }
}
