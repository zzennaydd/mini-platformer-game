using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 7f;

    private SpriteRenderer sprite;
    private PlayerLife playerlife;
    private bool isMoving = true;
    private Animator anim;

    private Rigidbody2D rb;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;

            }
            UpdateDirection();
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
    private void UpdateDirection()
    {
        if (currentWaypointIndex == 0)
        {
            sprite.flipX = false;
        }
        else
            sprite.flipX = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 contactPoint = collision.GetContact(0).point;
            Vector2 center = transform.position;


            if (contactPoint.y > center.y)
            {
                playerlife.AboveSnailOrSlime();
                isMoving = false;
                StopEnemy();
                anim.SetBool("isStopped", true);
            }
        }
    }
    private void StopEnemy()
    {

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
    }
}
