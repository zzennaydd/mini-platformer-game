using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
   // private BoxCollider2D coll;
    private Animator anim;
    private float dirX;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    [SerializeField] private LayerMask jumpableGround;

    [Header("Gravity")]
    public float baseGravity = 1.5f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;


    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 8f;
    private int maxJumps = 2;
    private int jumpsRemaining;

    [SerializeField] private AudioSource jumpSoundEffect;
   private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        //coll = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        Jump();
        GroundCheck();
        UpdateAnimationState();
        
    }
    private void FixedUpdate()
    {
        Gravity();
    }
    private void Gravity()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    private void Jump()
    {
        if (jumpsRemaining > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpSoundEffect.Play();
                jumpsRemaining--;
            }
            if (Input.GetButtonUp("Jump"))
            {
                rb.AddForce(Vector2.up * 0.5f, ForceMode2D.Impulse);
                
                jumpsRemaining--;
            }
        }
    }
    private void UpdateAnimationState()
    {
        if(dirX > 0f)
        {
            anim.SetBool("IsRunning", true);
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            anim.SetBool("IsRunning", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if(rb.velocity.y > .1f)
        {
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsRunning", false);
            
        }else if (rb.velocity.y < .1f)
        
        {
            anim.SetBool("IsJumping", false);
        }
        
    }

    private void GroundCheck()
    {
        //  return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f,jumpableGround);
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, jumpableGround))
        {
            jumpsRemaining = maxJumps;
        }
    }
}
