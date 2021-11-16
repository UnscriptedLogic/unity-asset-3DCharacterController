using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMovement : MovementTypeBase
{
    public float maxSlideVelMultiplier = 2.5f;
    public float maxJumpMultiplier = 2f;
    public float speedLerpTime = 5f;
    public float groundResistance = 1f;

    float currentSpeed;
    float currentJump; 

    float originalJump;
    float originalSpeed;

    [Space(20)]
    public float velocityToSlide = 6f;
    public float endSlideVelocity = 1f;

    bool isSliding;

    protected override void Start()
    {
        base.Start();

        inputSc.RegisterKeyBind(StartSlide, "Start Slide", triggerKey, TriggerType.GetKey);
        inputSc.RegisterKeyBind(EndSlide, "End Slide", triggerKey, TriggerType.GetKeyUp);
    }

    public override void Move()
    {
        /*continue to slide until
         * 1) we let go of the C key
         * OR
         * 2) our velocity is near zero
         * OR
         * 3) if player breaks the state by other states
         */

        /*
         * Additionally
         * when sliding and a jump is pressed, we keep the sliding state in the crouch
         * position. possibly in another class.
         */

        if (isSliding || inputSc.GetVelocity() >= endSlideVelocity)
        {
            ContinueSlide();
        } else
        {
            EndSlide();
        }
    }


    //This function is constantly getting called for now
    public void StartSlide()
    {
        bool isGrounded = inputSc.isGrounded();
        bool isMovingFastEnough = inputSc.GetVelocity() >= velocityToSlide;

        //if we are in the crouch position and we were not sliding before
        if (controllerSc.GetState() == MovementState.Crouch && !controllerSc.GetMovementStack().Contains(MovementState.Sliding))
        {
            if (isGrounded && isMovingFastEnough)
            {
                originalSpeed = moveSc.GetSpeed();
                originalJump = moveSc.GetJump();

                currentSpeed = originalSpeed * maxSlideVelMultiplier;
                currentJump = originalJump * maxJumpMultiplier;

                controllerSc.SetState(movementState);
                isSliding = true;
            }
        }
    }

    public void EndSlide()
    {
        controllerSc.StateEnded(movementState);

        moveSc.ResetAllMiscMovement();
        isSliding = false;
    }
    
    //The actual slide mechanic
    public void ContinueSlide()
    {
        float lerpTime = inputSc.isGrounded() ? speedLerpTime + groundResistance : speedLerpTime;

        moveSc.SetSpeed(currentSpeed);
        moveSc.SetJump(currentJump);

        currentSpeed -= lerpTime * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, 50f);
    }
}
