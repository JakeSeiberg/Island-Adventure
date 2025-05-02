using UnityEngine;

public class EnemyAutoMove : MonoBehaviour
{
    public float speed = 2f; 
    public Transform[] points; 
    public int roomIndex;
    
    private int targetIndex = 0; 
    private SpriteRenderer spriteRenderer;
    private bool movingLeft;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (points.Length > 0)
        {
            Move();
        }

        if (LeverTracker.leversSwitched[roomIndex] == true)
        {
            gameObject.SetActive(false);
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[targetIndex].position, speed * Time.deltaTime);

        movingLeft = points[targetIndex].position.x < transform.position.x;
        spriteRenderer.flipX = movingLeft;

        if (Vector3.Distance(transform.position, points[targetIndex].position) < 0.1f)
        {
            targetIndex++;

            if (targetIndex >= points.Length)
            {
                targetIndex = 0; 
            }
        }
    }
}
