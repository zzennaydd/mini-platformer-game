using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ropeSegment;
    public HingeJoint2D HingeJoint;
    [SerializeField] private GameObject pendulumEnd;
    private bool isAttached = false;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("pendulum") && !isAttached)
        {
            Debug.Log("trigger entered");
            AttachToRope();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("pendulum") && isAttached)
        {
            Debug.Log("trigger exited");
            DetachFromRope();
        }
    }
    void Start()
    {
      
        pendulumEnd = GameObject.FindWithTag("pendulumEnd");

        if (pendulumEnd == null)
        {
            Debug.LogError("pendulumEnd yok");
        }
    }
    void Update()
    {
        // Her karede boþluk tuþuna basýlýp basýlmadýðýný kontrol et
        if (Input.GetKeyDown(KeyCode.Space) && isAttached)
        {
            DetachFromRope();  // Eðer baðlýysa, boþluk tuþuna basýldýðýnda býrak
        }
    }
    void AttachToRope()
    {
        transform.parent = pendulumEnd.transform;
        if(transform.parent == pendulumEnd.transform)
        {
            Debug.Log("it worked");
        }
        HingeJoint = gameObject.AddComponent<HingeJoint2D>();
        HingeJoint.connectedBody = ropeSegment;
        if(HingeJoint.connectedBody == ropeSegment)
        {
            Debug.Log("HingeJoint.connectedBody = ropeSegment");
        }
        isAttached = true;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    void DetachFromRope()
    {
        if(HingeJoint != null)
        {
            Destroy(HingeJoint);
            Debug.Log("hingejoint destroyed");
            isAttached = false;
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            transform.parent = null;
        }
    }
}
