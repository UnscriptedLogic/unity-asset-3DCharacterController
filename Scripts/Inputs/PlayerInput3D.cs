using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput3D : PlayerInput
{
    Vector3 prevPosition;

    float displacement;
    float velocity;

    [Header("Refinements")]
    public float groundCheckOffset;
    public LayerMask groundLayer;

    private void Start()
    {
        prevPosition = transform.position;
    }

    public override void Update()
    {
        base.Update();

        prevPosition = transform.position;
    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, controller.height * groundCheckOffset);
    }

    public bool isGrounded(out RaycastHit hit)
    {
        bool value = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, controller.height * groundCheckOffset);
        hit = hitInfo;
        return value;
    }
    public float GetVelocity() { return velocity; }

    public void CalculateVelocity()
    {
        displacement = Vector3.Distance(transform.position, prevPosition);
        velocity = displacement / Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * (controller.height * groundCheckOffset));
    }
}
