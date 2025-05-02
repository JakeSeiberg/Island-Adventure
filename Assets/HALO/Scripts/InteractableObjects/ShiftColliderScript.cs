using UnityEngine;

public class ShiftColliderScript : MonoBehaviour
{
    public int colliderIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayShift.shiftColliders[colliderIndex] = true;
        }
    }




    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayShift.shiftColliders[colliderIndex] = false;
        }
    }

}
