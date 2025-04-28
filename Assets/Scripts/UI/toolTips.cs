using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    public CanvasGroup crashCanvasGroup;

    private Image image;
    private TMP_Text text;

    private List<string> tooltipQueue = new List<string>();
    private List<float> durationQueue = new List<float>();
    private bool isShowingTip = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        crashCanvasGroup.alpha = 0f;
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();

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

        StartCoroutine(fishingToolTip());
        StartCoroutine(fadeCanvas());

        StartCoroutine(MainWorldTooltips());
    }

    void Update()
    {
        if (!isShowingTip && tooltipQueue.Count > 0)
        {
            string nextTip = tooltipQueue[0];
            float duration = durationQueue[0];

            tooltipQueue.RemoveAt(0);
            durationQueue.RemoveAt(0);

            StartCoroutine(RunTip(nextTip, duration));
        }
    }

    private IEnumerator RunTip(string text, float duration)
    {
        isShowingTip = true;

        tooltipText.text = text;
        show();
        yield return StartCoroutine(AnimatePosition(rectTransform, hiddenPosition, visiblePosition, animationDuration));
        yield return new WaitForSeconds(duration);
        yield return StartCoroutine(AnimatePosition(rectTransform, visiblePosition, hiddenPosition, animationDuration));
        hide();

        isShowingTip = false;
    }

    private IEnumerator AnimatePosition(RectTransform rect, Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0f;
        Vector3 overshootPosition = end + new Vector3(0, -20f, 0);

        while (elapsedTime < duration)
        {
            float t = Mathf.Sin((elapsedTime / duration) * Mathf.PI * 0.5f);
            rect.anchoredPosition = Vector3.Lerp(start, overshootPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        float bounceDuration = duration * 0.5f;

        while (elapsedTime < bounceDuration)
        {
            float t = Mathf.Sin((elapsedTime / bounceDuration) * Mathf.PI * 0.5f);
            rect.anchoredPosition = Vector3.Lerp(overshootPosition, end, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = end;
    }

    public static void tip(string input, float waitTime)
    {
        Instance.tooltipQueue.Add(input);
        Instance.durationQueue.Add(waitTime);
    }

    public static void delayedToolTip(string tip, float waitTime)
    {
        Instance.StartCoroutine(Instance.delayedTooltipEnumerator(tip, waitTime));
    }

    private IEnumerator delayedTooltipEnumerator(string tip, float waitTime)
    {
        yield return new WaitForSeconds(2f);
        toolTips.tip(tip, waitTime);
    }

    private void hide()
    {
        if (image != null) image.enabled = false;
        if (text != null) text.enabled = false;
    }

    private void show()
    {
        if (image != null) image.enabled = true;
        if (text != null) text.enabled = true;
    }

    public static void changeScene()
    {
        Instance.hide();
        Instance.isShowingTip = false;
        Instance.tooltipQueue.Clear();
        Instance.durationQueue.Clear();
    }

    private IEnumerator fadeCanvas()
    {
        float duration = 2f;
        float elapsedTime = 0f;

        if (crashCanvasGroup != null)
            crashCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(1f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (crashCanvasGroup != null)
                crashCanvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / duration));
            yield return null;
        }

        if (crashCanvasGroup != null)
            crashCanvasGroup.alpha = 0f;
    }

    private IEnumerator MainWorldTooltips()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            
            if (playerData.startOfGame)
            {
                if (playerData.curScene == "MainWorld") toolTips.tip("Welcome to the island! Use WASD to move around, and E to interact with objects", 5f);
                playerData.startOfGame = false;
            }

            while (!playerData.hasAxe)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("You need to find an axe. Maybe there's one by the crashed plane", 7f);
                yield return new WaitForSeconds(20f);
            }

            while (!playerData.hasEnteredTreeGame)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("Interact with a tree to chop it down", 8f);
                yield return new WaitForSeconds(15f);
            }

            while (!playerData.hasOpenedShop)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("You can turn your wood and leaves into a bed at a work station. Maybe there's one by the beachside ruins", 8f);
                yield return new WaitForSeconds(20f);
            }

            while (!playerData.hasSlept)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("Interact with the bed to go to sleep", 8f);
                yield return new WaitForSeconds(20f);
            }

            while (!playerData.hasPickedUpAWorm)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("You're going to get hungry soon. Look for some bait to fish with. Maybe some worms", 7f);
                yield return new WaitForSeconds(15f);
            }

            while (!playerData.hasSpear)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("You need to find something to fish with. Maybe check around the crashed airplane", 7f);
                yield return new WaitForSeconds(15f);
            }

            while (!playerData.hasGoneFishing)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("You need to find a high spot to spearfish. Maybe there's a spot overlooking the water by the crashed plane", 7f);
                yield return new WaitForSeconds(15f);
            }
            
            while (!playerData.hasBoughtCampfire)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("You need to build a campfire. Gather some wood and go back to the workstation", 7f);
                yield return new WaitForSeconds(15f);
            }

            while (!playerData.hasPlacedFish)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("Interact with the cooking grate to place a fish to cook", 7f);
                yield return new WaitForSeconds(15f);
            }

            while (!playerData.hasBurnedWood)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("Interact with the campfire logs to add some wood", 7f);
                yield return new WaitForSeconds(15f);
            }

            while (!playerData.hasCookedFish)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("When the bar gets to the green zone, interact with it to flip it!", 7f);
                if (playerData.curScene == "MainWorld") toolTips.tip("If you flip it too early or too late, you'll ruin the fish!", 7f);
                yield return new WaitForSeconds(60f);
                if (playerData.curScene == "MainWorld") toolTips.tip("When the bar gets to the green zone, interact to collect it!", 7f);
                yield return new WaitForSeconds(15f);
            }

            while (!playerData.hasEatenFish)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("Press T to eat your fish", 7f);
                yield return new WaitForSeconds(15f);
            }

            while (!playerData.hasBoughtShelter)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("You can now progress your island! Explore and work on building the shelter!", 7f);
                yield return new WaitForSeconds(60f);
            }

            while (!playerData.hasBoughtShelter)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("You can now progress your island! Explore and work on building the shelter!", 7f);
                yield return new WaitForSeconds(60f);
            }

            while (!playerData.hasBoughtHull)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("You have built the shelter!", 4f);
                if (playerData.curScene == "MainWorld") toolTips.tip("Now work on building the boat hull and collecting parts from around the island and you'll be out of here in no time!", 7f);
                yield return new WaitForSeconds(60f);
            }

            while (!playerData.boatSail || !playerData.boatMotor || !playerData.boatGas)
            {
                yield return new WaitForSeconds(4f);
                int count = 0;
                if (!playerData.boatSail) count++;
                if (!playerData.boatMotor) count++;
                if (!playerData.boatGas) count++;
                string boatPartsTip = "";
                if (count > 1) boatPartsTip = "You need to find " + count + " more parts for the boat. Keep exploring the island!";
                else boatPartsTip = "You need to find " + count + " more part for the boat. Keep exploring the island!";

                if (playerData.curScene == "MainWorld") toolTips.tip(boatPartsTip, 7f);
                yield return new WaitForSeconds(40f);
            }

            while (!playerData.hasEscaped)
            {
                yield return new WaitForSeconds(5f);
                if (playerData.curScene == "MainWorld") toolTips.tip("You have everything you need! Go back to the boat to get out of here!", 8f);
                yield return new WaitForSeconds(35f);
            }
        }
    }

    private IEnumerator fishingToolTip()
    {
        yield return new WaitUntil(() => playerData.curScene == "Fishing");
        yield return new WaitForSeconds(2f);

        while (!playerData.hasThrownStrongSpear)
        {
            toolTips.tip("Aim with your mouse to aim the spear. Hold click to pull back spear, and release to throw it", 8);
            yield return new WaitForSeconds(10f);
            toolTips.tip("Press spacebar to throw your worms. They might attract more fish", 7);
            yield return new WaitForSeconds(20f);
            if (playerData.curScene == "Fishing")
                toolTips.tip("Press Escape to stop fishing", 5);
        }
    }
}
