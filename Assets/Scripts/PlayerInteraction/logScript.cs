using UnityEngine;

public class logScript : MonoBehaviour
{
    public void logCollected()
    {
        playerData.woodCount++;
        Destroy(this.gameObject);
    }
}
