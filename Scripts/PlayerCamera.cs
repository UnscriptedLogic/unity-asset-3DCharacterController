using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector2 mouseSens = new Vector2(200f, 200f);
    public Vector2 rotationClamp = new Vector2(-90f, 90f);

    public Transform camTransform;

    float xRotation = 0f;

    PlayerInput pInput;
    PlayerController pController;

    private void Start()
    {
        pInput = GetComponent<PlayerInput>();
        pController = GetComponent<PlayerController>();

    }

    private void Update()
    {
        DoLook();
    }

    void DoLook()
    {
        xRotation -= pInput.GetMouseInput(mouseSens).y;
        xRotation = Mathf.Clamp(xRotation, rotationClamp.x, rotationClamp.y);

        camTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * pInput.GetMouseInput(mouseSens).x);
    }
}
