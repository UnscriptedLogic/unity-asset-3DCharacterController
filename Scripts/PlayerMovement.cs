using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpHeight;

    public Rigidbody rb;

    // Update is called once per frame
    void Update()
    {

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 direction = zInput * transform.forward + xInput * transform.right;

        MoveCharacter(direction);
    }


    private void MoveCharacter(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * movementSpeed * Time.deltaTime));
    }


}
