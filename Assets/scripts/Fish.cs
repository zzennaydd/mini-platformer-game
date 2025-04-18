using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private DumbEnemy dumbEnemy;
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    private float speed;
    private Sprite icon;
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        speed = dumbEnemy.speed;
        icon = dumbEnemy.sprite;
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
            sprite.flipY = true;
        }
        else
            sprite.flipY = false;
    }
   
}
