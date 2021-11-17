using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SprintMovement : MovementTypeBase
{
    public float speedMultiplier = 2f;
    public float jumpMultiplier = 1.3f;

    public float transition = 20f;

    float originalSpeed;
    float currSpeed;

    bool isSprinting;

    protected override void Start()
    {
        base.Start();

        inputSc.RegisterKeyBind(Sprint, "Sprint", triggerKey, TriggerType.GetKeyDown);
        inputSc.RegisterKeyBind(ResetSprint, "Stop Sprinting", triggerKey, TriggerType.GetKeyUp);
    }

    public override void Move()
    {
        if (isGrounded())
        {
            if (isSprinting && inputSc.GetVelocity() > 0.05f)
            {
                Debug.Log(currSpeed);

                currSpeed += transition * Time.deltaTime;
                currSpeed = Mathf.Clamp(currSpeed, 0f, moveSc.GetMasterSpeed() * speedMultiplier);
                moveSc.SetSpeed(currSpeed);
            } else
            {
                moveSc.SetSpeed(originalSpeed);
                moveSc.SetJump(moveSc.GetJump() / jumpMultiplier);

                controllerSc.StateEnded(movementState);
            }
        }
    }

    public void Sprint()
    {
        originalSpeed = moveSc.GetSpeed();
        currSpeed = originalSpeed;
        controllerSc.SetState(movementState);
        moveSc.SetJump(moveSc.GetMasterJump() * jumpMultiplier);

        isSprinting = true;
    }

    public void ResetSprint()
    {
        isSprinting = false;
    }

    public override void ResetMovement()
    {
        currSpeed = moveSc.GetSpeed();
        originalSpeed = moveSc.GetSpeed();
    }
}
