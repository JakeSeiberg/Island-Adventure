using UnityEngine;

public class RandomEnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float moveTime = 2f;
    
    public float minX, maxX, minY, maxY; 

    private Vector2 movementDirection;
    private float moveTimer;
    private Rigidbody2D rb;
    private bool isIdle = false;

    private EnemyAnimationController animController; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponent<EnemyAnimationController>(); 
        PickNewDirection();
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0 && !isIdle)
        {
            PickNewDirection();
        }

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY)
        );
    }

    void FixedUpdate()
    {
        if (!isIdle)
        {
            rb.linearVelocity = movementDirection * speed; 
            animController.SetMoving(true, movementDirection.x); 
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animController.SetMoving(false, 0);
        }
    }

    void PickNewDirection()
    {
        int randomDir = Random.Range(0, 4);
        switch (randomDir)
        {
            case 0: movementDirection = Vector2.up; break;
            case 1: movementDirection = Vector2.down; break;
            case 2: movementDirection = Vector2.left; break;
            case 3: movementDirection = Vector2.right; break;
        }

        moveTimer = moveTime;
        isIdle = false; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isIdle = true;
            rb.linearVelocity = Vector2.zero;
            animController.SetMoving(false, 0); 

            Invoke("PickNewDirection", 0.5f); 
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isIdle = false;
            PickNewDirection(); 
        }
    }
}
