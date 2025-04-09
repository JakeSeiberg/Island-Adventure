using UnityEngine;

public class wormInteract : MonoBehaviour
{
    // Static method that can be called without an instance of wormInteract
    public void wormCollected()
    {
        // Destroy the GameObject this script is attached to
        playerData.wormCount++;
        Destroy(this.gameObject);
    }
}