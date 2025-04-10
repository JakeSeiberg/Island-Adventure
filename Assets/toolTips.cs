using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class toolTips : MonoBehaviour
{
    public static toolTips Instance { get; private set; }

    private RectTransform rectTransform; 
    private Vector3 hiddenPosition = new Vector3(0, 340, 0); 
    private Vector3 visiblePosition = new Vector3(0, 196, 0); 
    private float animationDuration = 0.5f;

    public TMP_Text tooltipText;

    private bool toolTipActive = false;

    private Image image;
    private TMP_Text text;

    void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();

        Instance = this;

        Canvas parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas != null)
        {
            DontDestroyOnLoad(parentCanvas.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = hiddenPosition; 
        tooltipText = GetComponentInChildren<TMP_Text>();
        if (playerData.startOfGame)
        {
            StartCoroutine(startOfGameTip());
            playerData.startOfGame = false;
        }
        StartCoroutine(spearTooltip());
        StartCoroutine(fishingToolTip());
        StartCoroutine(treeToolTips());
    }

    private IEnumerator startOfGameTip()
    {
        playerData.startOfGame = false;
        yield return new WaitForSeconds(4f);
        if (playerData.curScene == "MainWorld")
        {
            toolTips.tip("Welcome to the island! Use WASD to move around, and E to interact with objects", 5f);
        }
    }

    public static void tip(string input, float waitTime)
    {
        if (!Instance.toolTipActive)
        {
            Instance.toolTipActive = true;
            Instance.show();
            Instance.StartCoroutine(Instance.ShowToolTip(input, waitTime));
        }
    }

    private IEnumerator ShowToolTip(string input, float waitTime)
    {
        tooltipText.text = input;
        
        yield return StartCoroutine(AnimatePosition(rectTransform, hiddenPosition, visiblePosition, animationDuration));

        yield return new WaitForSeconds(waitTime);

        yield return StartCoroutine(AnimatePosition(rectTransform, visiblePosition, hiddenPosition, animationDuration));
        Instance.toolTipActive = false;
        Instance.hide();
    }

    private IEnumerator AnimatePosition(RectTransform rect, Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0f;
        Vector3 overshootPosition = end + new Vector3(0, -20f, 0); 

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f); 
            rect.anchoredPosition = Vector3.Lerp(start, overshootPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        float bounceDuration = duration * 0.5f; 
        while (elapsedTime < bounceDuration)
        {
            float t = elapsedTime / bounceDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f); 
            rect.anchoredPosition = Vector3.Lerp(overshootPosition, end, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = end; 
    }

    private IEnumerator spearTooltip()
    {
        while (playerData.hasPickedUpAWorm == false)
        {
            Debug.Log("worm check");
            yield return new WaitForSeconds(5f);
            Debug.Log("worm check after");
        }
        Debug.Log("worm picked up- spear tooltip loaded");

        yield return new WaitForSeconds(10f);
        Debug.Log("Spear tooltip started");

        while (!playerData.hasSpear)
        {   
            if (playerData.curScene == "MainWorld")
            {
                toolTips.tip("You need to find something to fish with. Maybe check around the crashed airplane",7);

                yield return new WaitForSeconds(25f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
        

        while (!playerData.hasGoneFishing)
        {   
            if (playerData.curScene == "MainWorld")
            {
                toolTips.tip("You need to find a high spot to spearfish. Maybe there's a spot overlooking the water by the crashed plane",7);

                yield return new WaitForSeconds(25f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private IEnumerator fishingToolTip()
    {
        while (playerData.curScene != "Fishing")
        {
            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(2f);

        while (!playerData.hasThrownStrongSpear)
        {
            toolTips.tip("Aim with your mouse to aim the spear. Hold click to pull back spear, and release to throw it",8);

            yield return new WaitForSeconds(10f);

            toolTips.tip("Press spacebar to throw your worms. They might attract more fish",7);

            yield return new WaitForSeconds(20f);

            toolTips.tip("Press Escape to stop fishing",5);
        }
    }

    private IEnumerator treeToolTips()
    {
        while (!playerData.hasAxe)
        {
            yield return new WaitForSeconds(5f);
            if (playerData.hasThrownStrongSpear)
            {
                if (playerData.curScene == "MainWorld")
                {
                    toolTips.tip("You need to find an axe. Maybe there's one by the crashed plane", 7f);
                }
            }
            
            yield return new WaitForSeconds(15f);
        }

        while (!playerData.hasEnteredTreeGame)
        {
            yield return new WaitForSeconds(5f);
            if (playerData.curScene == "MainWorld")
            {
                toolTips.tip("Interact with a tree to chop it down", 8f);
            }
            yield return new WaitForSeconds(15f);
        }
    }

    public static void changeScene()
    {
        Instance.hide();
        Instance.toolTipActive = false;
    }

    private void hide()
    {
        image.enabled = false; 
        text.enabled = false;
    }

    private void show()
    {
        image.enabled = true; 
        text.enabled = true;
    }

    public static void delayedToolTip(string tip, float waitTime)
    {
        Instance.StartCoroutine(Instance.delayedTooltipEnumerator(tip, waitTime));
    }

    private IEnumerator delayedTooltipEnumerator(string tip, float waitTime)
    {
        yield return new WaitForSeconds(2f);
        toolTips.tip(tip, waitTime);

        toolTips.tip("THIs is the tip",100f);
    }
}