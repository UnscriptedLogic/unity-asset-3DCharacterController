using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 10f;
    public float jump = 5f;
    [Space]
    public Vector2 mouseSens = new Vector2(200f, 200f);
    public Vector2 mouseClamp = new Vector2(-90f, 90f);

    [Header("Components")]
    public Rigidbody rb;
    public Transform cam;
    public InputScript inputSc;
    public PlayerCameraScript cameraSc;

    MovementScript moveSc;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        moveSc = new SprintMovementScript(inputSc, rb, speed / 5f, jump);
    }

    private void Update()
    {
        cameraSc.RotateCamera(inputSc.GatherMouseAxis(mouseSens), mouseClamp, cam);
        moveSc.MoveCharacter(transform, speed, inputSc.GatherInputAxis().normalized, rb);
    }
}
