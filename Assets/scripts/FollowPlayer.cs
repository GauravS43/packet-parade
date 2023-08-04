using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    private PlayerMovement p;

    Vector3[] offsetArr = new[] {
        new Vector3(0, 1, -7), //down
        new Vector3(-1, 0, -7), //right
        new Vector3(0, -1, -7), //above
        new Vector3(1, 0, -7), //left
    };

    Quaternion[] rotatePresets = new[]
    {
        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(0, 0, 90),
        Quaternion.Euler(0, 0, 180),
        Quaternion.Euler(0, 0, 270)
    };

    public bool interpolate;
    private float elapsedTime = 0f;
    private float desiredDuration = 0.25f;
    private int oldState = 0;
    private int state;

    void Start()
    {
        Application.targetFrameRate = 60;
        player = GameObject.Find("Player Controller").GetComponent<Transform>();
        p = GameObject.Find("Player Controller/Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (p.interpolateFlag)
        {
            p.interpolateFlag = false;

            state = p.state;

            interpolate = true;
            elapsedTime = 0;
        }

        if (!interpolate)
        {
            transform.position = player.position + offsetArr[p.oldState];
        }
        else
        {
            elapsedTime += Time.deltaTime;
            float percentageCompleted = elapsedTime / desiredDuration;
            
            if (percentageCompleted > 1)
            {
                percentageCompleted = 1;
                interpolate = false;
                oldState = state;
            }

            transform.position = Vector3.Lerp(player.position + offsetArr[oldState], player.position + offsetArr[state], percentageCompleted);
            transform.rotation = Quaternion.Lerp(rotatePresets[oldState], rotatePresets[state], percentageCompleted);
        }
    }
}
