using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ropeSegment;
    public HingeJoint2D HingeJoint;
    private bool isAttached = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("pendulum"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isAttached)
                {
                    AttachToRope();
                }
                else
                {
                    DetachFromRope();
                }
            }
        }
    }
    
          

    void AttachToRope()
    {
        HingeJoint = gameObject.AddComponent<HingeJoint2D>();
        HingeJoint.connectedBody = ropeSegment;
        isAttached = true;
        GetComponent<PlayerMovement>().enabled = false;
    }
    void DetachFromRope()
    {
        if(HingeJoint != null)
        {
            Destroy(HingeJoint);
            isAttached = false;
            GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
