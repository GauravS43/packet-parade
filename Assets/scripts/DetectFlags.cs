using UnityEngine;

public class DetectFlags : MonoBehaviour
{
    public GameManager Game;

    public Transform downCheck;
    public LayerMask goalFlag;
    public LayerMask deathFlag;
    public GameObject controller;

    void Update()
    {

        if (Physics.CheckSphere(downCheck.position, 1f, deathFlag))
        {
            Game.EndGame();
        }


        if (Physics.CheckSphere(downCheck.position, 1f, goalFlag))
        {
            controller.SetActive(false);
            Game.WinLevel();
        }
    }
}
