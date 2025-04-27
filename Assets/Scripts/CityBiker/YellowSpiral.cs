using UnityEngine;
using System.Collections;

public class YellowSpiral : MonoBehaviour
{

    private SpriteRenderer boxRenderer;
    private float rotationSpeed = 25f;
    private bool spinning = false;
    //static public float inversionDuration = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxRenderer = GetComponent<SpriteRenderer>();
        boxRenderer.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spinning)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    public void spin()
    {
        StartCoroutine(spinMe());
    }

    private IEnumerator spinMe()
    {
        boxRenderer.enabled = true;
        spinning = true;

        yield return new WaitForSeconds(BeerPickup.inversionDuration);

        boxRenderer.enabled = false;
        spinning = false;

    }
}
