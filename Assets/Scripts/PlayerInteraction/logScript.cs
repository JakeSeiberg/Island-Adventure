using UnityEngine;

public class logScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void logCollected()
    {
        playerData.woodCount++;
        Destroy(this.gameObject);
    }
}
