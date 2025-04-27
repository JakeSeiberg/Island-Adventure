using UnityEngine;
using System.Collections;

public class RedBox : MonoBehaviour
{
    private SpriteRenderer boxRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxRenderer = GetComponent<SpriteRenderer>();
        boxRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {
        StartCoroutine(damage());
    }

    private IEnumerator damage()
    {
        boxRenderer.enabled = true;

        yield return new WaitForSeconds(1f);

        boxRenderer.enabled = false;

    }

}
