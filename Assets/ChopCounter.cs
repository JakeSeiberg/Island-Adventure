using TMPro;
using UnityEngine;
using System.Collections;

public class ChopCounter : MonoBehaviour
{
    public TextMeshPro counterText;
    public SpriteRenderer checkmarkSprite;
    public SpriteRenderer xmarkSprite;

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

        if (hits == hitsNeeded)
        {
            Debug.Log("Tree chopped!");
            // TODO: Trigger end logic here
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
