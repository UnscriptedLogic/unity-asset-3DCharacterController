using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CrouchMovement : MovementTypeBase
{
    [Header("Settings")]
    public float crouchHeight = 1f;
    public float crouchJumpController = 3f;
    public float crouchSpeedController = 5f;
    [Space]
    public KeyCode crouchKey = KeyCode.C;

    bool isCrouching;
    bool alreadyCalled;

    protected override void Start()
    {
        base.Start();

        pInput.RegisterKeyBind(Crouch, "Crouch", crouchKey, TriggerType.GetKey);
        pInput.RegisterKeyBind(UnCrouch, "UnCrouch", crouchKey, TriggerType.GetKeyUp);
    }

    public void Update()
    {

    }

    public void Crouch()
    {
        if (!isCrouching)
        {
            charController.height = crouchHeight;

            isCrouching = true;
            pController.SetState(movementState);
        }

        if (isCrouching && pInput.isGrounded() && !alreadyCalled)
        {
            pMovement.SetSpeed(pMovement.GetSpeed() / crouchSpeedController);
            pMovement.SetJump(pMovement.GetJump() / crouchJumpController);

            alreadyCalled = true;
        }
    }

    public void UnCrouch()
    {
        if (isCrouching)
        {
            charController.height = pMovement.initalHeight;
            
            if (pInput.isGrounded())
            {
                pMovement.SetJump(pMovement.GetJump() * crouchJumpController);
                pMovement.SetSpeed(pMovement.GetSpeed() * crouchSpeedController);

                isCrouching = false;
                pController.SetState(MovementState.Normal);

                alreadyCalled = false;

            }
        }
    }
}
