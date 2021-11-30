using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTypeBase : MonoBehaviour
{
    public MovementState movementState;

    public KeyCode triggerKey;

    protected PlayerInput3D inputSc;
    protected PlayerController controllerSc;
    protected PlayerMovement moveSc;
    protected CharacterController charController;

    protected virtual void Start()
    {
        inputSc = GetComponent<PlayerInput3D>();
        controllerSc = GetComponent<PlayerController>();
        moveSc = GetComponent<PlayerMovement>();
        charController = GetComponent<CharacterController>();

        moveSc.AddToMovementList(this);
        inputSc = controllerSc.playerInput;
    }

    public virtual void Move()
    {

    }

    public MovementState GetState() { return movementState; }

    public bool isGrounded()
    {
        return inputSc.isGrounded();
    }

    public float GetVelocity()
    {
        return inputSc.GetVelocity();
    }

    public virtual void ResetMovement()
    {

    }
}
