using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform parentTransform;

    public Transform downCheck;
    public Transform rightCheck;
    public Transform aboveCheck;
    public Transform leftCheck;

    public LayerMask groundMask;
    public LayerMask deathMask;

    float speed = 30f;
    float groundDistance = 0.1f;

    bool isGrounded;

    public int state = 0;
    public int oldState = 0;
    public bool interpolateFlag = false;


    Vector3 velocity = new Vector3(0, 0, 0);

    Vector3[] groundForce = new[] {
        new Vector3(0f, -1f, 0f), //down
        new Vector3(1f, 0f, 0f), //right
        new Vector3(0f, 1f, 0f), //above
        new Vector3(-1f, 0f, 0f) //left
        };

    Vector3[] jumpForce = new[] {
        new Vector3(0f, 10f, 0f), //down
        new Vector3(-10f, 0f, 0f), //right
        new Vector3(0f, -10f, 0f), //above
        new Vector3(10f, 0f, 0f) //left
    };

    void Update()
    {
        bool hitDownWall = Physics.CheckSphere(downCheck.position, groundDistance, groundMask);
        bool hitRightWall = Physics.CheckSphere(rightCheck.position, groundDistance, groundMask);
        bool hitAboveWall = Physics.CheckSphere(aboveCheck.position, groundDistance, groundMask);
        bool hitLeftWall = Physics.CheckSphere(leftCheck.position, groundDistance, groundMask);

        if (hitDownWall) state = 0;
        else if (hitRightWall) state = 1;
        else if (hitAboveWall) state = 2;
        else if (hitLeftWall) state = 3;
        else state = -1;

        if (state != oldState && state > -1)
        {
            interpolateFlag = true;
            if (oldState == 0 || oldState == 2) {
                velocity.y = 0;
            }
            else {
                velocity.x = 0;
            }

            oldState = state;
        }

        //can probably replace isgrounded
        isGrounded = state > -1;

        //if (isGrounded)
        //{
        //    velocity = groundForce[state];
        //}


        //horizontal movement
        float x = Input.GetAxis("Horizontal");

        Vector3[] moveForce = new[] {
        new Vector3(x, 0f, 1f), //down
        new Vector3(0f, x, 1f), //right
        new Vector3(-x, 0f, 1f), //above
        new Vector3(0f, -x, 1f) //left
        };

        Vector3 moveVector;

        if (isGrounded) {
            moveVector = moveForce[state];
        }
        else
        {
            moveVector = moveForce[oldState];
        }

        controller.Move(moveVector * speed * Time.deltaTime);


        //vertical movement
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity = jumpForce[state];
        }

        velocity += -(jumpForce[oldState]) * Time.deltaTime;


        controller.Move(velocity * Time.deltaTime);
    }
}
