using UnityEngine;

public class spearPickupScript : MonoBehaviour
{
    public Renderer spearFishingRenderer;

    void Start()
    {
        if (playerData.hasSpear)
        {
            spearFishingRenderer.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
        else
        {
            spearFishingRenderer.gameObject.SetActive(false);
        }
        Color color = spearFishingRenderer.material.color;
        color.a = 0.25f;
        spearFishingRenderer.material.color = color;

    }

    public void hasSpear()
    {
        playerData.hasSpear = true;
        spearFishingRenderer.gameObject.SetActive(true);

        Destroy(this.gameObject);
    }

    
}


