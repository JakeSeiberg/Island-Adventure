using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    public void SetMoving(bool isMoving, float directionX)
    {
        animator.SetBool("isMoving", isMoving); 

        if (directionX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (directionX > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
