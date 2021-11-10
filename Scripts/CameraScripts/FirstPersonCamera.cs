using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : CameraScript
{
    private float mouseX;
    private float mouseY;
    public Vector2 mouseInput;

    public Vector2 mouseSens = new Vector2(100f, 100f);

    public Vector2 clampRot = new Vector2(-90f, 90f); 

    Transform camTransform;

    float xRotation = 0f;

    public FirstPersonCamera(Transform _cameraTransform, Vector2 _ms)
    {
        camTransform = _cameraTransform;
        mouseSens = _ms;
    }

    public override void CameraMovement(Transform entity)
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens.y * Time.deltaTime;
        mouseInput = new Vector2(mouseX, mouseY);

        //Rotation Calculation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, clampRot.x, clampRot.y);

        //Rotation Itself
        camTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        entity.Rotate(Vector3.up * mouseX);
    }
}
