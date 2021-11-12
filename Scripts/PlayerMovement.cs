using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float movementSpeed;
    public float jumpHeight;

    [Space]
    public float crouchHeight;
    public float crouchSpeedController;
    public float crouchJumpController;

    public float gravity = -20f;

    [HideInInspector] public float initalHeight;
    float moveSpeed;
    float jump;

    Vector3 velocity;

    bool isCrouching;

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

        pInput.RegisterKeyBind(Jump, "Jump", KeyCode.Space, TriggerType.GetKey);
    }

    private void FixedUpdate()
    {
        DoMovementType();
    }

    public void DoMovementType()
    {
        if (pController.movementState != MovementState.Normal)
        {
            for (int i = 0; i < movementTypes.Count; i++)
            {
                if (movementTypes[i].GetState() == pController.movementState)
                {
                    movementTypes[i].Move();
                }
            }
        }

        charController.Move(pInput.GetDirectionalInput() * moveSpeed * Time.deltaTime);

        if (pInput.isGrounded() && velocity.y <= 0)
        {
            velocity.y = -2f;
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
        }
    }

    //Setters
    public void SetSpeed(float speed) { moveSpeed = speed; }
    public void SetJump(float height) { jump = height; }
    public void SetControllerHeight(float height) { charController.height = height; }

    //Getters
    public float GetSpeed() { return moveSpeed; }
    public float GetJump() { return jump; }
    public float GetControllerHeight() { return charController.height; }

    //Resetters
    public void ResetSpeed() { moveSpeed = movementSpeed; }
    public void ResetJump() { jump = jumpHeight; }
    public void ResetControllerHeight() { charController.height = initalHeight; }
}
