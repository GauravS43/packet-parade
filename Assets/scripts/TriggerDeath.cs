using UnityEngine;

public class TriggerDeath : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Main/GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.EndGame();
    }

}
