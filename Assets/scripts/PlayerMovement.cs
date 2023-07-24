using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform parentTransform;

    public Transform downCheck;
    public Transform rightCheck;
    public Transform aboveCheck;
    public Transform leftCheck;
    Vector3 oldPosition = new Vector3(-1, -1, -1);

    public LayerMask groundMask;
    public LayerMask deathMask;

    private float speed = 25f;
    private float groundDistance = 0.1f;

    //declared public as followPlayer.cs uses them
    [HideInInspector]
    public int state = 0;
    [HideInInspector]
    public int oldState = 0;
    [HideInInspector]
    public bool interpolateFlag = false;


    Vector3 velocity = new Vector3(0, 0, 0);

    Vector3[] jumpForce = new[] {
        new Vector3(0f, 10f, 0f), //down
        new Vector3(-10f, 0f, 0f), //right
        new Vector3(0f, -10f, 0f), //above
        new Vector3(10f, 0f, 0f) //left
    };

    Vector3[] groundForce = new[] {
        new Vector3(0f, -1f, 0f), //down
        new Vector3(1f, 0f, 0f), //right
        new Vector3(0f, 1f, 0f), //above
        new Vector3(-1f, 0f, 0f) //left
        };

    float switchThreshold = 0f;

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
        else
        {
            //deltatime threshold, for interpolate, if state > -1 then refresh threshold 
            switchThreshold += Time.deltaTime;
            state = -1;
        }

        if (state != oldState && state > -1 && switchThreshold > 0.2) 
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

        //horizontal movement (input)
        float x = Input.GetAxis("Horizontal");

        Vector3[] moveForce = new[] {
        new Vector3(x, 0f, 0f), //down
        new Vector3(0f, x, 0f), //right
        new Vector3(-x, 0f, 0f), //above
        new Vector3(0f, -x, 0f) //left
        };

        

        controller.Move(moveForce[oldState] * 0.75f * speed * Time.deltaTime);


        //vertical movement (input)

        //prevents gravity build up when grounded
        if (state > -1)
        {
           velocity.x = groundForce[state].x;
           velocity.y = groundForce[state].y;
        }

        //gravity (if player holds down jump, they will go farther)
        if (Input.GetButton("Jump"))
        {
            velocity += (-1.9f) * (jumpForce[oldState]) * Time.deltaTime;
        }
        else
        {
            velocity += (-2.5f)* (jumpForce[oldState]) * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && (state > -1))
        {
            switchThreshold = 0;
            velocity.x = 1.5f * jumpForce[state].x;
            velocity.y = 1.5f * jumpForce[state].y;
        }

        //forwards movement (no-input)
        if (oldPosition.z == transform.position.z)
        {
            velocity.z = 0;
        }


        if (velocity.z < 40f)
        {
            velocity.z += 10f * Time.deltaTime;
        }

        if (velocity.z > 40f)
        {
            velocity.z = 40f;
        }

        oldPosition = transform.position;

        controller.Move(velocity * Time.deltaTime);
    }
}
