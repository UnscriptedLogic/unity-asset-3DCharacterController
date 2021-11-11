using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MoveModBase
{
    Rigidbody rb;
    float jumpForce;

    public Jump(Rigidbody _rb, float _jforce)
    {
        rb = _rb;
        jumpForce = _jforce;
    }

    public override void Trigger()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    public override void Trigger(ref float variable)
    {
        
    }
}
