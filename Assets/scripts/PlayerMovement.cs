using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator anim;
    private float dirX = 0f;

   [SerializeField] private LayerMask jumpableGround;
   [SerializeField] private float moveSpeed = 7f;
   [SerializeField] private float jumpForce = 7f;
   
   private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
       
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();

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

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f,jumpableGround);
    }
}
