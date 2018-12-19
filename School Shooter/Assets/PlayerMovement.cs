using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed;
    public float jumpForce;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        Vector3 forwardMovement = transform.forward * Input.GetAxis("Vertical") * movementSpeed;
        Vector3 sideMovement = transform.right * Input.GetAxis("Horizontal") * movementSpeed / 2;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            forwardMovement /= 3;
            sideMovement /= 3;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                forwardMovement *= 2;
                sideMovement *= 2;
            }
        }
        rb.velocity = forwardMovement + sideMovement + transform.up * -9.8f;
    }
}
