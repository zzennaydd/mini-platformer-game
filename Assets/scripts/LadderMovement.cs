using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float speed = 8f;
    private float vertical;
    private bool isClimbing;
    private bool isNearLadder;
    private Animator anim;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent <Animator>();
    }
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if(isNearLadder)
        {
            isClimbing = true;
        }

    }
    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
            anim.SetBool("isClimbingLadder", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
        }
        else
        {
            rb.gravityScale = 1f;
            anim.SetBool("isClimbingLadder", false);
        }
         
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ladder"))
        {
            isNearLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            isNearLadder = false;
            isClimbing = false;
        }

    }
}
