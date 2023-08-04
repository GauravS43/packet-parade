using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Transform downCheck;
    private Transform leftCheck;
    private Transform rightCheck;
    private Transform upCheck;
    private Transform[] groundCheck = new Transform[4];

    public LayerMask groundMask;
    public LayerMask downMask;
    public LayerMask leftMask;
    public LayerMask rightMask;
    public LayerMask upMask;

    //declared public as followPlayer.cs uses them
    [HideInInspector] public int state = 0;
    [HideInInspector] public int oldState = 0;
    [HideInInspector] public bool interpolateFlag = false;

    private float groundDistance = 0.1f;
    private bool heldJump = false;

    private Vector3 oldPosition = new Vector3(-1, -1, -1);
    private Vector3 velocity = new Vector3(0, 0, 0);

    private Vector3[] jumpForce = new[] {
        new Vector3(0f, 10f, 0f), //down
        new Vector3(-10f, 0f, 0f), //right
        new Vector3(0f, -10f, 0f), //above
        new Vector3(10f, 0f, 0f) //left
    };

    private Vector3[] groundForce = new[] {
        new Vector3(0f, -1f, 0f), //down
        new Vector3(1f, 0f, 0f), //right
        new Vector3(0f, 1f, 0f), //above
        new Vector3(-1f, 0f, 0f) //left
    };

    private Vector3[] moveForce = new[] {
        new Vector3(20f, 0f, 0f), //down
        new Vector3(0f, 20f, 0f), //right
        new Vector3(-20f, 0f, 0f), //above
        new Vector3(0f, -20f, 0f) //left
    };

    void Start()
    {
        controller = GameObject.Find("Player Controller").GetComponent<CharacterController>();
        downCheck = GameObject.Find("Player Controller/DownCheck").GetComponent<Transform>();
        leftCheck = GameObject.Find("Player Controller/LeftCheck").GetComponent<Transform>();
        rightCheck = GameObject.Find("Player Controller/RightCheck").GetComponent<Transform>();
        upCheck = GameObject.Find("Player Controller/UpCheck").GetComponent<Transform>();

        groundCheck[0] = downCheck;
        groundCheck[1] = rightCheck;
        groundCheck[2] = upCheck;
        groundCheck[3] = leftCheck;
    }

    void Update()
    {
        bool hitDownWall = Physics.CheckSphere(downCheck.position, groundDistance, downMask);
        bool hitRightWall = Physics.CheckSphere(rightCheck.position, groundDistance, rightMask);
        bool hitAboveWall = Physics.CheckSphere(upCheck.position, groundDistance, upMask);
        bool hitLeftWall = Physics.CheckSphere(leftCheck.position, groundDistance, leftMask);

        if (hitDownWall) state = 0;
        else if (hitRightWall) state = 1;
        else if (hitAboveWall) state = 2;
        else if (hitLeftWall) state = 3;
        else state = -1;

        bool grounded = (state > -1) || Physics.CheckSphere(groundCheck[oldState].position, groundDistance, groundMask);

        //changes camera and resets momentum when wall hit
        if (state != oldState && state > -1)
        {
            interpolateFlag = true;

            if (oldState == 0 || oldState == 2) velocity.y = 0;
            else velocity.x = 0;

            oldState = state;
        }

        //HORIZONTAL MOVEMENT
        controller.Move(moveForce[oldState] * Input.GetAxis("Horizontal") * Time.deltaTime);

        //VERTICAL MOVEMENT
        //prevents gravity build up when grounded
        if (grounded)
        {
            velocity.x = groundForce[oldState].x;
            velocity.y = groundForce[oldState].y;
        }

        //gravity (if player holds down jump, they will go farther)
        if (Input.GetButton("Jump") && heldJump)
        {
            velocity += (-2.2f) * (jumpForce[oldState]) * Time.deltaTime;
        }
        else
        {
            heldJump = false;
            velocity += (-2.8f) * (jumpForce[oldState]) * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            heldJump = true;
            velocity.x = 1.5f * jumpForce[oldState].x;
            velocity.y = 1.5f * jumpForce[oldState].y;
        }

        //FORWARD MOVEMENT
        //resets speed if hits wall
        if (oldPosition.z == transform.position.z)
        {
            velocity.z = 0;
        }

        if (velocity.z < 38f)
        {
            velocity.z += 10f * Time.deltaTime;
        }

        if (velocity.z > 38f)
        {
            velocity.z = 38f;
        }

        oldPosition = transform.position;

        controller.Move(velocity * Time.deltaTime);
    }
}

