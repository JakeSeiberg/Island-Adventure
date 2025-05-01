using UnityEngine;
using System.Collections;

public class wormInteract : MonoBehaviour
{
    private Renderer rend;
    public void wormCollected()
    {
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
            float distance = Vector3.Distance(transform.position, PlayerMovement.currentPlayerPos);

            if (distance > 10f)
            {
                rend.enabled = false;
            }
            else
            {
                rend.enabled = true;
            }

            yield return new WaitForSeconds(2f);
        }
    }
}


