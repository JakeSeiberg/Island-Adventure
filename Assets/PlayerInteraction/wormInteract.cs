using UnityEngine;

public class wormInteract : MonoBehaviour
{
    // Static method that can be called without an instance of wormInteract
    public static void wormCollected()
    {
        //delete gameobject
        GameObject worm = GameObject.FindGameObjectWithTag("Worm");
        if (worm != null)
        {
            Destroy(worm);
        }

    }
}