using TMPro;
using UnityEngine;
using System.Collections;

public class ChopCounter : MonoBehaviour
{
    public TextMeshPro counterText;
    public SpriteRenderer checkmarkSprite;
    public SpriteRenderer xmarkSprite;

    public AxeScript axeController;
    public AccuracyBarMover accuracyBarMover;

    public Transform treeToFall; // Drag the tree GameObject here
    public float fallDuration = 3f; // How long the fall should take

    public int hits = 0;
    public int hitsNeeded = 5;

    void Start()
    {
        checkmarkSprite.enabled = false;
        xmarkSprite.enabled = false;
        counterText.text = hits + "/" + hitsNeeded;

    }

    public void RegisterHit()
    {
        hits++;

        if (hits > hitsNeeded)
            hits = hitsNeeded;

        counterText.text = hits + "/" + hitsNeeded;

        StartCoroutine(FlashCheckmark());

        if (accuracyBarMover != null && hits < hitsNeeded)
        {
            accuracyBarMover.speed += 1f; // Increase speed by 1 after each successful chop
        }

        if (hits == hitsNeeded)
        {
            Debug.Log("Tree chopped!");
            playerData.treeChopped = true;
            playerData.woodCount++;

            if (axeController != null)
                axeController.enabled = false;

            if (treeToFall != null)
                StartCoroutine(FallTree());

            if (accuracyBarMover != null)
                accuracyBarMover.enabled = false;
            
            toolTips.tip("Tree Chopped! Press [ESC]", 100f);
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

    private IEnumerator FallTree()
    {
        yield return new WaitForSeconds(1.2f);

        Quaternion startRotation = treeToFall.rotation;
        Quaternion endRotation = Quaternion.Euler(
            treeToFall.eulerAngles.x - 89f,
            treeToFall.eulerAngles.y,
            treeToFall.eulerAngles.z
        );

        float elapsed = 0f;

        while (elapsed < fallDuration)
        {
            float t = elapsed / fallDuration;
            treeToFall.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        treeToFall.rotation = endRotation;
    }

}
