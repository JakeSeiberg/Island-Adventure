using UnityEngine;

public class spearPickupScript : MonoBehaviour
{
    public Renderer spearFishingRenderer;

    void Start()
    {
        //disable spearfishingingrenderer gameobject
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
        // Destroy the GameObject this script is attached to
        playerData.hasSpear = true;
        spearFishingRenderer.gameObject.SetActive(true);

        Destroy(this.gameObject);
    }

    
}


