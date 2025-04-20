using UnityEngine;
using UnityEngine.UI;

public class fireTimerUIScript : MonoBehaviour
{
    public GameObject physicalFireTimer;

    private Image fireTimerImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireTimerImage = physicalFireTimer.GetComponent<Image>();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            float fill = Mathf.Lerp(0.094f, 0.927f, playerData.fireValue / 200f);
            fireTimerImage.fillAmount = fill;
        }
    }

    public void enable()
    {
        gameObject.SetActive(true);
    }

    public void disable()
    {
        gameObject.SetActive(false);
    }
}
