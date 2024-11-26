using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator anim;
    private float dirX;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 7f;
    // [SerializeField] private float acceleration = 3f;
    // [SerializeField] private float deceleration = 2f;
    private float fallGravityScale = 17f;
    private float jumpGravityScale = 5f;

    [SerializeField] private AudioSource jumpSoundEffect;
   private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        rb.gravityScale = jumpGravityScale;
    }
    private void Update()
    {
        
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpSoundEffect.Play();
           if(rb.velocity.y < 0) 
           {
                rb.gravityScale = fallGravityScale;
                
           }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) 
            {
                rb.gravityScale = fallGravityScale;
                
            }
            else
            {
                rb.gravityScale = jumpGravityScale;
                
            } 
        }   
        UpdateAnimationState();
    }
    /* void FixedUpdate()
    {
        if (moveDirection != Vector2.zero) // Hareket varsa hýzlandýr
        {
            currentSpeed += acceleration * Time.fixedDeltaTime;
        }
        else // Hareket yoksa yavaþlat
        {
            currentSpeed -= deceleration * Time.fixedDeltaTime;
        }

        // Hýzý sýnýrlandýr
        currentSpeed = Mathf.Clamp(currentSpeed, 0, moveSpeed);

        // Hareketi uygulama
        rb.velocity = moveDirection * currentSpeed; 
    } */
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

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f,jumpableGround);
    }
}
