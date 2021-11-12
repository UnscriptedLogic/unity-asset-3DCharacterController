using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchMovement : MovementTypeBase
{
    [Header("Settings")]
    public float crouchHeight = 1f;
    public float crouchJumpController = 3f;
    public float crouchSpeedController = 5f;

    bool isCrouching;

    protected override void Start()
    {
        base.Start();

        pInput.RegisterKeyBind(Crouch, "Crouch", KeyCode.C, TriggerType.GetKey);
        pInput.RegisterKeyBind(UnCrouch, "UnCrouch", KeyCode.C, TriggerType.GetKeyUp);
    }

    public void Crouch()
    {
        if (!isCrouching)
        {
            charController.height = crouchHeight;

            isCrouching = true;
            pController.SetState(MovementState.Crouch);
        }

        if (isCrouching && pInput.isGrounded())
        {
            pMovement.SetSpeed(pMovement.movementSpeed / crouchSpeedController);
            pMovement.SetJump(pMovement.jumpHeight / crouchJumpController);
        }
    }

    public void UnCrouch()
    {
        if (isCrouching)
        {
            charController.height = pMovement.initalHeight;
            pMovement.ResetJump();
            pMovement.ResetSpeed();

            isCrouching = false;
            pController.SetState(MovementState.Normal);
        }
    }
}
