using UnityEngine;
using UnityEngine.UI;

public class FireTimerUIScript : MonoBehaviour
{
    public GameObject physicalFireTimer;

    private Image fireTimerImage;

    public Image[] fireTimerImages;

    public bool isActive;

    private bool previouslyActive;

    void Start()
    {
        fireTimerImage = physicalFireTimer.GetComponent<Image>();
        isActive = false;
        previouslyActive = !isActive;
    }

    void FixedUpdate()
    {
        
        setVisibility();

        if (isActive)
        {
            float fill = Mathf.Lerp(0.094f, 0.927f, playerData.fireValue / 200f);
            fireTimerImage.fillAmount = fill;
        }
            
    }

    private void setVisibility()
    {
        if (isActive != previouslyActive)
        {
            foreach (Image image in fireTimerImages)
            {
                Color color = image.color;
                color.a = isActive ? 1f : 0f;
                image.color = color;
            }

            previouslyActive = isActive;
        }
    }
}
