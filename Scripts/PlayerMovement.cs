using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpHeight;

    public Rigidbody rb;

    private MovementScript movementScript;

    private void Start()
    {
        movementScript = new RBBasedMovement(rb, transform);
    }

    void Update()
    {

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(xInput, 0f, zInput);


        movementScript.PerformMove(movementSpeed, direction);
    }
}
