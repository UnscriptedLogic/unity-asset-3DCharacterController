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
    public InputScript inputSc;

    MovementScript moveSc;

    float xRotation = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        moveSc = new SprintMovementScript(inputSc, 0);
    }

    private void Update()
    {
        RotateCamera(inputSc.GatherMouseAxis(mouseSens));
        moveSc.MoveCharacter(transform, speed, inputSc.GatherInputAxis().normalized, rb);
    }

    private void RotateCamera(Vector2 mouseDirection)
    {
        xRotation -= mouseDirection.y;
        xRotation = Mathf.Clamp(xRotation, mouseClamp.x, mouseClamp.y);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseDirection.x);
    }
}
