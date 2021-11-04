using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseXSens = 100f;
    public float mouseYSens = 100f;

    public Vector2 clampRot = new Vector2(-90f, 90f); 

    public Transform camTransform;

    float xRotation = 0f;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseXSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseYSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, clampRot.x, clampRot.y);

        camTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
