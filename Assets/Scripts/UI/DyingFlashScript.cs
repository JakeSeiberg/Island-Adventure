using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DyingFlashScript : MonoBehaviour
{
    private bool dying = false;

    public GameObject flashImage;
    private Image flashImageComponent;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashImageComponent = flashImage.GetComponent<Image>();
        flashImageComponent.enabled = false;
    }

    void FixedUpdate()
    {
       //Debug.Log("Dying: " + dying + " Air: " + playerData.air);
        if (playerData.hungerValue <= 20 || (playerData.air <= 40 && playerData.curScene == "MainWorld"))
        {
            if (!dying)
            {
                dying = true;  
                StartCoroutine(Flash());
            }
        }
        else
        {
            dying = false;
        }
    }

    private IEnumerator Flash()
    {
        while (dying)
        {
            flashImageComponent.enabled = true;
            yield return new WaitForSeconds(.7f);
            flashImageComponent.enabled = false;
            yield return new WaitForSeconds(.7f);
        }
    }

    
}
