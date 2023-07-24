using UnityEngine;

public class TriggerDeath : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerExit(Collider other)
    {
        gameManager.EndGame();
    }

}
