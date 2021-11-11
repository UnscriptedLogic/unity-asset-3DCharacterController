using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementScript
{
    public virtual void MoveCharacter(Transform _transform, float speed, Vector3 direction, Rigidbody rb)
    {
        rb.MovePosition(_transform.position + (direction * speed * Time.deltaTime));
    }
}
