using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMult;
    bool canJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Variables")]
    public float groundDrag;
    public float playerHeight;
    public LayerMask isGround;
    bool onGround;

    [Header("Other")]
    public Transform orientation;

    //public Animator playerAnimator;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    public static Vector3 currentPlayerPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Teleport the player
        rb.position = playerData.playerPosition;

        // Clear any initial velocity just in case
        rb.linearVelocity = Vector3.zero;

        // Optional but good practice
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        currentPlayerPos = rb.position;

        canJump = true;
            
    }

    void Update()
    {
        
        currentPlayerPos = rb.position;
        Vector3 tmpPos = transform.position;
        tmpPos.y = transform.position.y + 3f; //dont change 3
        onGround = Physics.Raycast(tmpPos, Vector3.down, playerHeight*0.5f + 0.2f, isGround);

        //should split these into new clases at some point
        MyInput();
        dragCheck();
        limitSpeed();
        //checkAnimation();
    }
    
    /*
    private void checkAnimation(){
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            playerAnimator.SetBool("isWalking", true);
            Debug.Log("here");
        }
        else{
            playerAnimator.SetBool("isWalking", false);
        }
    }*/
    
    
    private void dragCheck()
    {
        if(onGround){
            //rb.linearDamping = groundDrag;
        }
        else{
            rb.linearDamping = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && canJump && onGround){
            canJump = false;
            jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }
    }

    private void MovePlayer(){
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(onGround){
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        }

        else if (!onGround){
            rb.AddForce(moveDirection * moveSpeed * 10f * airMult, ForceMode.Force);          
        }

    }

    private void limitSpeed(){
        Vector3 flatVelo = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if(flatVelo.magnitude > moveSpeed){
            Vector3 controlledVelo = flatVelo.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(controlledVelo.x, rb.linearVelocity.y, controlledVelo.z);
        }
    }

    private void jump(){
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void resetJump(){
        canJump = true;
    }
    
}
