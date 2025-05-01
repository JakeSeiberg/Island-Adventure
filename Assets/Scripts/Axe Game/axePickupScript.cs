using UnityEngine;

public class axePickupScript : MonoBehaviour
{
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
