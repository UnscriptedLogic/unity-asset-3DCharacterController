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
    public float transitionTime;
    public float checkValidDistance = 1.15f;

    float originalSpeed;
    bool isCrouching;

    protected override void Start()
    {
        base.Start();

        inputSc.RegisterKeyBind(Crouch, "Crouch", triggerKey, TriggerType.GetKeyDown);
        inputSc.RegisterKeyBind(UnCrouch, "UnCrouch", triggerKey, TriggerType.GetKeyUp);
    }

    public override void Move()
    {
        if (isGrounded())
        {
            if (isCrouching)
            {
                moveSc.SetSpeed(Mathf.Lerp(moveSc.GetSpeed(), moveSc.GetMasterSpeed() / crouchSpeedController, transitionTime * Time.deltaTime));
            }
            else
            {
                ResetCrouch();
            }
        }
    }

    public void Crouch()
    {
        if (!isCrouching)
        {
            originalSpeed = moveSc.GetSpeed();
            charController.height = crouchHeight;
            
            moveSc.SetJump(moveSc.GetJump() / crouchJumpController);
            controllerSc.SetState(movementState);
            isCrouching = true;
        }
    }

    public void UnCrouch()
    {
        isCrouching = false;
    }

    public void ResetCrouch()
    {
        //Checks for a valid height to uncrouch
        if (!Physics.Raycast(transform.position, transform.up, out RaycastHit hitInfo, checkValidDistance))
        {
            moveSc.ResetControllerHeight();

            moveSc.SetJump(moveSc.GetJump() * crouchJumpController);
            moveSc.SetSpeed(originalSpeed);

            controllerSc.StateEnded(movementState);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Vector3.up * checkValidDistance);
    }
}
