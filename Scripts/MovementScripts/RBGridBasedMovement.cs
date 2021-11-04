using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBGridBasedMovement : MovementScript
{
    public Rigidbody rb;
    public Transform entity;

    public RBGridBasedMovement(Rigidbody _rb, Transform _entity)
    {
        rb = _rb;
        entity = _entity;
    }

    public override void PerformMove(float speed)
    {

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(xInput, 0f, zInput);

        rb.MovePosition(entity.position + (direction * speed * Time.deltaTime));
    }

    public override void PerformMove(float speed, Vector3 direction)
    {
        throw new System.NotImplementedException();
    }
}
