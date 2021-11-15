using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float movementSpeed;
    public float jumpHeight;

    [Space(20)]
    public float gravity = -20f;
    public float speedToJumpMul = 20f;

    [HideInInspector] public float initalHeight;
    float moveSpeed;
    float jump;

    Vector3 velocity;

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
        moveSpeed = movementSpeed;
        jump = jumpHeight;

        pInput.RegisterKeyBind(Jump, "Jump", KeyCode.Space, TriggerType.GetKeyDown);
    }

    public void Update()
    {
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

        charController.Move(pInput.GetDirectionalInput() * moveSpeed * Time.deltaTime);

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
            jump = jumpHeight * ((moveSpeed / 100f) * speedToJumpMul);
            jump = Mathf.Clamp(jump, 0f, jumpHeight * 2f);

            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
            pController.SetState(MovementState.Jumping);
        }
    }

    public void AddToMovementList(MovementTypeBase movementType)
    {
        movementTypes.Add(movementType);
    }

    //Setters
    public void SetSpeed(float speed) { moveSpeed = speed; }
    public void SetJump(float height) { jump = height; }
    public void SetControllerHeight(float height) { charController.height = height; }

    //Getters
    public float GetSpeed() { return moveSpeed; }
    public float GetJump() { return jump; }
    public float GetControllerHeight() { return charController.height; }

    public CharacterController GetCharacterController() { return charController; }
    public float GetMasterSpeed() { return movementSpeed; }
    public float GetMasterJump() { return jumpHeight; }

    //Resetters
    public void ResetSpeed() { moveSpeed = movementSpeed; }
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
