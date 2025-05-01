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

    public Transform treeToFall; 
    public float fallDuration = 3f; 

    public int hits = 0;
    public int hitsNeeded = 5;

    public AudioManager audioManager;

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
            accuracyBarMover.speed += 1f; 
        }

        if (hits == hitsNeeded)
        {

            audioManager.playTree();

            playerData.treeChopped = true;
            playerData.brokenTrees.Add(playerData.currentTreeID);

            if (axeController != null)
                axeController.enabled = false;

            if (treeToFall != null)
                StartCoroutine(DoFallThenChange());

            if (accuracyBarMover != null)
                accuracyBarMover.enabled = false;

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
    private IEnumerator SwitchScene(){
        SceneSwitcher.changeScene();
        yield break;
    }


    IEnumerator DoFallThenChange()
    {
        yield return StartCoroutine(FallTree());
        yield return StartCoroutine(SwitchScene());
    }

}
