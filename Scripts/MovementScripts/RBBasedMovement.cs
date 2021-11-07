using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBBasedMovement : MovementScript
{
    public Rigidbody rb;
    public Transform entity;

    public RBBasedMovement(Rigidbody _rb, Transform _entity)
    {
        rb = _rb;
        entity = _entity;
    }

    public override void PerformMove(float speed)
    {

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 direction = zInput * entity.forward + xInput * entity.right;

        rb.MovePosition(entity.position + (direction * speed * Time.deltaTime));
    }

    public override void PerformMove(float speed, Vector3 direction)
    {
        rb.MovePosition(entity.position + (direction * speed * Time.deltaTime));
    }
}
