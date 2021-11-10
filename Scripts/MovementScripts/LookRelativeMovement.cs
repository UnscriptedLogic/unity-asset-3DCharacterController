using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRelativeMovement : MovementScript
{
    public Rigidbody rb;
    public Transform entity;

    float sprint;

    public LookRelativeMovement(Rigidbody _rb, Transform _entity, float _sprint = 1.5f)
    {
        rb = _rb;
        entity = _entity;
        sprint = _sprint;
    }

    public override void PerformMove(float speed)
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 direction = zInput * entity.forward + xInput * entity.right;

        Sprint(ref speed);

        rb.MovePosition(entity.position + (direction * speed * Time.deltaTime));
    }

    public override void PerformMove(float speed, Vector3 direction)
    {
        Sprint(ref speed);

        rb.MovePosition(entity.position + (direction * speed * Time.deltaTime));
    }

    public void Sprint(ref float _sprint)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _sprint *= sprint;
        }
    }
}
