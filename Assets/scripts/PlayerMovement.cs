using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public Transform rightCheck;

    public LayerMask groundMask;

    float speed = 20f;
    float gravity = -10f;
    float groundDistance = 0.4f;

    bool isGrounded;

    int state = 0;
    
    Vector3 velocity;

    void Update()
    {
        //have array of transforms
        // use mod arithmetic to determine what needs to be checked

        bool hitRightWall = Physics.CheckSphere(rightCheck.position, groundDistance, groundMask);
        if (hitRightWall)
        {
            Debug.Log("Hit Wall");
            state = 1;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            if (state == 0)
            {
                velocity.y = -2f;
            }
            if (state == 1)
            {
                velocity.x = 2f;
            }
        }

        //ground: velocity.y = -2f
        //right: velocity.x = 2f
        //left: velocity.x = -2f
        //up: velocity.y = 2f


        float x = Input.GetAxis("Horizontal");

        Vector3 movement_vector;

        if (state == 0){
            movement_vector = new Vector3(x, 0, 1);
        }
        else
        {
            movement_vector = new Vector3(0, x, 1);
        }

        //ground new Vector3(x, 0, 1);
        //right: new Vector3(0, x, 1);
        //left  new Vector3(0, -x, 1);
        //up new Vector3 (-x, 0, 1);

        controller.Move(movement_vector * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            if (state == 0)
            {
                velocity.y = 10f;
            }
            if (state == 1)
            {
                velocity.x = -10f;
            }
        }

        //ground: velocity.y = 10f
        //right: velocity.x = -10f
        //left: velocity.x = 10f
        //up: velocity.y = -10f

        if (state == 0)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        if (state == 1)
        {
            velocity.x -= gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }


    //public Rigidbody rb;
    ////FixedUpdate is specifically used for physics
    //void FixedUpdate()
    //{
    //    //forward force
    //    rb.AddForce(0, 0, forwardSpeed * Time.deltaTime);

    //    //if (Input.GetKey("d"))
    //    //{
    //    //    rb.AddForce(sideSpeed * Time.deltaTime, 0, 0);
    //    //}
    //    //if (Input.GetKey("a"))
    //    //{
    //    //    rb.AddForce(-sideSpeed * Time.deltaTime, 0, 0);
    //    //}
    //}
}
