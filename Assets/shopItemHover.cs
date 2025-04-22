using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shopItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;  // The button that handles clicks
    public Image itemImage; // The image that represents the item sprite
    public Outline outline; // The outline component for hover effect
    public bool isPurchased = false; // Hook this up from your actual shop data

    void Start()
    {
        if (outline != null)
            outline.enabled = false;  // Make sure outline is off by default
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPurchased && outline != null)
        {
            outline.enabled = true;  // Show the outline on hover
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (outline != null)
        {
            outline.enabled = false;  // Hide the outline when the mouse leaves
        }
    }

    public void OnClick()
    {
        // Handle button click (can also be attached to the Button.onClick directly)
        Debug.Log("Item Clicked: " + itemImage.name);
    }
}
