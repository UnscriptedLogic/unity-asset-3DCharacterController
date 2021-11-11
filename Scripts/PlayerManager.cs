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
    public InputScript inputScript;

    float xRotation = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateCamera(inputScript.GatherMouseAxis(mouseSens));
        MoveCharacter(speed, inputScript.GatherInputAxis(), rb);
    }

    private void RotateCamera(Vector2 mouseDirection)
    {
        xRotation -= mouseDirection.y;
        xRotation = Mathf.Clamp(xRotation, mouseClamp.x, mouseClamp.y);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseDirection.x);
    }

    public void MoveCharacter(float speed, Vector3 direction, Rigidbody rb)
    {
        if (inputScript.inputs[0].keyActive)
        {
            speed *= 2f;
        }

        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

}
