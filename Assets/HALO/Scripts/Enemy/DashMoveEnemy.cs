using UnityEngine;
using System.Collections;

public class DashingEnemy : MonoBehaviour
{
    public float dashSpeed = 6f;  
    public float idleTime = 1.5f; 
    public Transform[] points;    

    public int roomIndex;

    private int targetIndex = 0; 
    private bool isDashing = false;
    private SpriteRenderer spriteRenderer;
    private bool movingLeft;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = points[0].position; 
        StartCoroutine(DashRoutine());
    }

    void Update()
    {
        if (points.Length > 0)
        {
            spriteRenderer.flipX = movingLeft;
        }

        if (LeverTracker.leversSwitched[roomIndex] == true)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator DashRoutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(idleTime); 

            int nextIndex = (targetIndex + 1) % points.Length; 
            Vector3 targetPosition = points[nextIndex].position;

            isDashing = true;
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, dashSpeed * Time.deltaTime);
                movingLeft = targetPosition.x < transform.position.x;
                yield return null; 
            }
            
            isDashing = false;
            targetIndex = nextIndex; 
        }
    }
}
