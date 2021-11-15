using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    Idle,
    Moving,
    Jumping,
    Crouch,
    Sliding,
    Sprinting
}

public class PlayerController : MonoBehaviour
{
    Stack<MovementState> movementStack = new Stack<MovementState>();
    List<MovementState> toRemove = new List<MovementState>();

    public MovementState movementState;

    [Header("Components")]
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public Rigidbody rb;

    private void Start()
    {
        movementStack.Push(MovementState.Idle);
        movementStack.Push(MovementState.Moving);
    }

    private void Update()
    {
        switch (movementState)
        {
            case MovementState.Idle:
                playerMovement.ResetAllBasicMovement();
                break;
            case MovementState.Moving:
                playerMovement.ResetAllBasicMovement();
                break;
            case MovementState.Jumping:
                break;
            case MovementState.Sliding:
                break;
            case MovementState.Crouch:
                break;
            case MovementState.Sprinting:
                break;
            default:
                break;
        }

        if (toRemove.Count > 0)
        {
            int last = toRemove.Count - 1;
            if (toRemove[last] == movementState)
            {
                StateEnded(movementState);
                toRemove.RemoveAt(last);
            }

        }
    }

    public Stack<MovementState> GetMovementStack()
    {
        return movementStack;
    }

    public MovementState GetState()
    {
        return movementState;
    }

    public void SetState(MovementState state)
    {
        movementState = state;

        movementStack.Push(state);
    }

    public void StateEnded(MovementState state)
    {
        if (movementStack.Peek() == state)
        {
            movementStack.Pop();
            movementState = movementStack.Peek();
        } else
        {
            if (!toRemove.Contains(state))
            {
                toRemove.Insert(0, state);
            }
        }
    }
}
