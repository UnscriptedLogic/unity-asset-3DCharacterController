using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float movementSpeed;
    public float jumpHeight;
    public float transitionTime;

    [Space(20)]
    public float gravity = -20f;
    public float speedToJumpMul = 20f;

    [HideInInspector] public float initalHeight;

    float baseJump;

    float currentMoveSpeed;
    float jump;
    float jumpAccelerator;

    Vector3 velocity;
    Vector3 inputVector;
    Vector3 movementVector;

    PlayerController pController;
    PlayerInput pInput;
    CharacterController charController;
    List<MovementTypeBase> movementTypes = new List<MovementTypeBase>();

    private void Start()
    {
        pController = GetComponent<PlayerController>();
        pInput = GetComponent<PlayerInput>();
        charController = GetComponent<CharacterController>();

        initalHeight = charController.height;
        currentMoveSpeed = movementSpeed;
        jump = jumpHeight;

        baseJump = jump;

        pInput.RegisterKeyBind(Jump, "Jump", KeyCode.Space, TriggerType.GetKeyDown);
    }

    public void Update()
    {
        jumpAccelerator = (pInput.GetVelocity() / 100f) * speedToJumpMul;
        jump = baseJump + jumpAccelerator;
        jump = Mathf.Clamp(jump, 0f, jumpHeight * 2f);


        DoMovementType();
    }

    public void DoMovementType()
    {
        for (int i = 0; i < movementTypes.Count; i++)
        {
            if (movementTypes[i].GetState() == pController.movementState)
            {
                movementTypes[i].Move();
            }
        }

        inputVector = pInput.GetDirectionalInput() * currentMoveSpeed * Time.deltaTime;
        charController.Move(inputVector);

        if (pInput.isGrounded() && velocity.y <= 0)
        {
            velocity.y = -2f;

            if (pController.GetState() == MovementState.Jumping)
            {
                pController.StateEnded(MovementState.Jumping);
            }
        }

        velocity.y += gravity * Time.deltaTime;

        charController.Move(velocity * Time.deltaTime);
    }

    public void GetMovementTypes()
    {
        if (movementTypes.Count <= 0)
        {
            foreach (MovementTypeBase movement in GetComponents<MovementTypeBase>())
            {
                movementTypes.Add(movement);
            }
        }
    }

    public void Jump()

    {
        if (pInput.isGrounded())
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
            pController.SetState(MovementState.Jumping);
        }
    }

    public void AddToMovementList(MovementTypeBase movementType)
    {
        movementTypes.Add(movementType);
    }

    //Setters
    public void SetSpeed(float speed) { currentMoveSpeed = speed; }
    public void SetJump(float height) { jump = height; }
    public void SetControllerHeight(float height) { charController.height = height; }

    //Getters
    public float GetSpeed() { return currentMoveSpeed; }
    public float GetJump() { return jump; }
    public float GetJumpAccelerator() { return jumpAccelerator; }
    public float GetBaseJump() { return baseJump; }
    public float GetControllerHeight() { return charController.height; }

    public CharacterController GetCharacterController() { return charController; }
    public float GetMasterSpeed() { return movementSpeed; }
    public float GetMasterJump() { return jumpHeight; }

    //Resetters
    public void ResetSpeed() { currentMoveSpeed = movementSpeed; }
    public void ResetJump() { jump = jumpHeight; }
    public void ResetControllerHeight() { charController.height = initalHeight; }
    public void ResetAllBasicMovement()
    {
        ResetSpeed();
        ResetJump();
        ResetControllerHeight();
    }
    public void ResetAllMiscMovement()
    {
        for (int i = 0; i < movementTypes.Count; i++)
        {
            movementTypes[i].ResetMovement();
        }
    }
}
