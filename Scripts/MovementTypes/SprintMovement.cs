using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SprintMovement : MovementTypeBase
{
    public float speedMultiplier = 2f;
    public float jumpMultiplier = 1.3f;
    public KeyCode sprintKey = KeyCode.LeftShift;

    bool isCalled;
    bool isAwaitingLanding;

    protected override void Start()
    {
        base.Start();

        pInput.RegisterKeyBind(Sprint, "Sprint", sprintKey, TriggerType.GetKey);
        pInput.RegisterKeyBind(ResetSprint, "Stop Sprinting", sprintKey, TriggerType.GetKeyUp);
    }

    public void Update()
    {
        if (isAwaitingLanding)
        {
            if (pInput.isGrounded())
            {
                pMovement.SetSpeed(pMovement.GetSpeed() / speedMultiplier);
                pMovement.SetJump(pMovement.GetJump() / jumpMultiplier);
                isCalled = false;
                isAwaitingLanding = false;
            }
        }
    }

    public void Sprint()
    {
        if (pInput.isGrounded() && !isCalled)
        {
            pMovement.SetSpeed(pMovement.GetSpeed() * speedMultiplier);
            pMovement.SetJump(pMovement.GetJump() * jumpMultiplier);

            pController.SetState(movementState);

            isCalled = true;
        }
    }

    public void ResetSprint()
    {
        if (pInput.isGrounded())
        {
            pMovement.SetSpeed(pMovement.GetSpeed() / speedMultiplier);
            pMovement.SetJump(pMovement.GetJump() / jumpMultiplier);

            pController.SetState(MovementState.Normal);

            isCalled = false;
        } else
        {
            isAwaitingLanding = true;
        }
    }
}
