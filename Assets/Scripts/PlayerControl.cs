using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour
{

    Rigidbody2D playerRB;
    Animator playerAnimator;

    public float moveSpeed = 1f;
    public float jumpSpeed = 1f, jumpFrequency = 1f, nextJumpTime;

    bool facingRight = true;
    public bool isGrounded = false;
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        OnGroundCheck();

        if(playerRB.velocity.x < 0 && facingRight){
            //flip face
            FlipFace();
        }
        else if(playerRB.velocity.x >0 && !facingRight){
            //flip face
            FlipFace();
        }

        if(CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad)){
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();
        }
    }

    void FixedUpdate()
    {

    }

    void HorizontalMove()
    {
        playerRB.velocity = new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal") * moveSpeed, playerRB.velocity.y);
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
    }

    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }

    void Jump()
    {
        playerRB.AddForce( new Vector2(0f,jumpSpeed));
        // playerRB.velocity = new Vector2(playerRB.velocity.y ,Input.GetAxis("Vertical") * jumpSpeed);
        // playerAnimator.SetFloat("playerJump", Mathf.Abs(playerRB.velocity.y));
    }

    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position , groundCheckRadius, groundCheckLayer);
        playerAnimator.SetBool("isGroundedAnim", isGrounded);
    }
}
