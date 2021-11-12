using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    Normal,
    Sliding,
    Crouch
}

public class PlayerController : MonoBehaviour
{
    public MovementState movementState;

    [Header("Components")]
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;

    public void UpdateState()
    {
        switch (movementState)
        {
            case MovementState.Normal:
                movementState = MovementState.Normal;
                break;
            case MovementState.Sliding:
                movementState = MovementState.Sliding;
                break;            
            case MovementState.Crouch:
                movementState = MovementState.Crouch;
                break;
            default:
                movementState = MovementState.Normal;
                break;
        }
    }

    public MovementState GetState()
    {
        return movementState;
    }

    public void SetState(MovementState state)
    {
        movementState = state;
    }
}
