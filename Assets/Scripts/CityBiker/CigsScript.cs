using UnityEngine;
using System.Collections;

public class CigsScript : MonoBehaviour
{
    private SpriteRenderer cigsRenderer;
    public Sprite[] cigsImages;

    static public int cigsCount;

    private Timer time;
    private bool full;
    private bool timeLost;
    private TextController pointsText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cigsRenderer = GetComponent<SpriteRenderer>();
        cigsCount = 0;
        time = FindFirstObjectByType<Timer>();
        timeLost = false;
        pointsText = FindFirstObjectByType<TextController>();

    }

    // Update is called once per frame
    void Update()
    {
        cigsRenderer.sprite = cigsImages[cigsCount % cigsImages.Length];
        if(cigsCount > 7 && !timeLost){
            full = true;
            timeLost = true;
            StartCoroutine(cigReset());
        }
    }

    private IEnumerator cigReset(){
        if (full == true){
            time.loseTime(-15); 
            pointsText.points("+15");
            full = false;
        }
        yield return new WaitForSeconds(1.5f);
        cigsCount = 0;
        timeLost = false;
    }
}
