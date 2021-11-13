using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    Idle,
    Normal,
    Sliding,
    Crouch,
    Sprinting
}

public class PlayerController : MonoBehaviour
{
    public MovementState movementState;

    [Header("Components")]
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;

    private void Update()
    {
        //if (playerInput.GetDirectionalInput().magnitude <= 0)
        //{
        //    movementState = MovementState.Idle;
        //}
    }

    public void UpdateState()
    {
        switch (movementState)
        {
            case MovementState.Idle:
                playerMovement.ResetAllBasicMovement();
                break;

            case MovementState.Normal:
                playerMovement.ResetAllBasicMovement();

                break;

            case MovementState.Sliding:

                break;    
                
            case MovementState.Crouch:
                break;  
                
            case MovementState.Sprinting:
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

        UpdateState();
    }
}
