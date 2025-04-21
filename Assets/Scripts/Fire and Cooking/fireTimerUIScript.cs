using UnityEngine;
using UnityEngine.UI;

public class fireTimerUIScript : MonoBehaviour
{
    public GameObject physicalFireTimer;

    private Image fireTimerImage;

    public Image[] fireTimerImages;

    public bool isActive;

    private bool previouslyActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireTimerImage = physicalFireTimer.GetComponent<Image>();
        isActive = false;
        previouslyActive = !isActive;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        SetVisibility();

        if (isActive)
        {
            float fill = Mathf.Lerp(0.094f, 0.927f, playerData.fireValue / 200f);
            fireTimerImage.fillAmount = fill;
        }
        //Debug.Log("Fire timer active: " + isActive);
            
    }

    private void SetVisibility()
    {
        if (isActive != previouslyActive)
        {
            foreach (Image image in fireTimerImages)
            {
                Color color = image.color;
                color.a = isActive ? 1f : 0f; // Fully visible if active, fully transparent if not
                image.color = color;
            }

            previouslyActive = isActive;
        }
    }
}
