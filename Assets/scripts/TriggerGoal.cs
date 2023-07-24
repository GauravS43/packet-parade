using UnityEngine;

public class TriggerGoal : MonoBehaviour
{
    public GameManager Game;
    public PlayerMovement p;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player Controller")
        {
            p.enabled = false;
            Game.WinLevel();
        }
    }
}
