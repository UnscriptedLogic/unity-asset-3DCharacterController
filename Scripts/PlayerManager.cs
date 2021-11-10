using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Attributes")]
    public float movementSpeed;
    public float sprintSpeed = 1.5f;

    public Vector2 mouseSens = new Vector2(100f, 100f);

    [Header("Components")]
    public Rigidbody rb;
    public Transform cameraTransform;
    public InputScript inputScript;

    MovementScript movementScript;
    CameraScript cameraScript;

    private void Start()
    {
        movementScript = new LookRelativeMovement(rb, transform, sprintSpeed);
        cameraScript = new FirstPersonCamera(cameraTransform, mouseSens);
    }

    private void Update()
    {
        movementScript.PerformMove(movementSpeed);
        cameraScript.CameraMovement(transform);
    }
}
