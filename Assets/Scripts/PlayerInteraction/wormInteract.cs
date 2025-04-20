using UnityEngine;
using System.Collections;

public class wormInteract : MonoBehaviour
{
    private Renderer rend;
    // Static method that can be called without an instance of wormInteract
    public void wormCollected()
    {
        // Destroy the GameObject this script is attached to
        playerData.wormCount++;
        Destroy(this.gameObject);
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(visualization());   
    }

    private IEnumerator visualization()
    {
        while (true)
        {
            // Calculate the distance between the worm and the player
            float distance = Vector3.Distance(transform.position, PlayerMovement.currentPlayerPos);

            // Turn the renderer on or off based on the distance
            if (distance > 10f)
            {
                rend.enabled = false; // Turn off the renderer
            }
            else
            {
                rend.enabled = true; // Turn on the renderer
            }

            yield return new WaitForSeconds(2f); // Check every 0.5 seconds
        }
    }
}


