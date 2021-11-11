using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 10f;
    public float jump;
    [Space]
    public Vector2 mouseSens = new Vector2(200f, 200f);
    public Vector2 mouseClamp = new Vector2(-90f, 90f);

    [Header("Components")]
    public Rigidbody rb;
    public Transform cam;

    float xRotation = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 direction = transform.forward * zInput + transform.right * xInput;


        float mouseX = Input.GetAxis("Mouse X") * mouseSens.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens.y * Time.deltaTime;
        Vector2 mouseDir = new Vector3(mouseX, mouseY);

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint();
        }

        RotateCamera(mouseDir);

        MoveCharacter(speed, direction, rb);
    }

    private void Sprint()
    {
        throw new NotImplementedException();
    }

    private void Crouch()
    {
        throw new NotImplementedException();
    }

    private void Jump()
    {
        throw new NotImplementedException();
    }

    private void RotateCamera(Vector2 mouseDirection)
    {
        xRotation -= mouseDirection.y;
        xRotation = Mathf.Clamp(xRotation, mouseClamp.x, mouseClamp.y);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseDirection.x);
    }

    private void MoveCharacter(float speed, Vector3 direction, Rigidbody rb)
    {
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

}
