using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTypeBase : MonoBehaviour
{
    public MovementState movementState;

    protected PlayerInput pInput;
    protected PlayerController pController;
    protected PlayerMovement pMovement;
    protected CharacterController charController;

    protected virtual void Start()
    {
        pInput = GetComponent<PlayerInput>();
        pController = GetComponent<PlayerController>();
        pMovement = GetComponent<PlayerMovement>();
        charController = GetComponent<CharacterController>();
    }

    public virtual void Move()
    {

    }

    public MovementState GetState() { return movementState; }
}
