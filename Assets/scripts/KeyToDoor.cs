using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeytoDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject key;
    private ItemCollector itemCollector;
    private BoxCollider2D coll;
    private bool doorOpened = false;

    void Start()
    {
        itemCollector = FindObjectOfType<ItemCollector>();
        coll = door.GetComponent<BoxCollider2D>();

        if (coll != null)
        {
            coll.isTrigger = false;
           
        }
    }

    void Update()
    {
        if (key != null && itemCollector !=null)
        {
            if (itemCollector.keyCollected)
            {
                doorOpened = true;
                coll.isTrigger = true;
                
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("door"))
        {
            if(doorOpened)
            {
                Destroy(collision.gameObject);
                
            }
        }
    }
}