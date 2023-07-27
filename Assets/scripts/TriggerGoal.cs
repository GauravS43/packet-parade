using UnityEngine;

public class TriggerGoal : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerMovement player;

    private void Start()
    {
        gameManager = GameObject.Find("Main/GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player Controller/Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player Controller")
        {
            player.enabled = false;
            gameManager.WinLevel();
        }
    }
}
