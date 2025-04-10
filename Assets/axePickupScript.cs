using UnityEngine;

public class axePickupScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerData.hasAxe)
        {
            Destroy(this.gameObject);
        }
    }

    public void hasAxe()
    {
        playerData.hasAxe = true;
        Destroy(this.gameObject);
    }
}
