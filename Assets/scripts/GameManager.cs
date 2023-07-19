using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void WinLevel()
    {
        Debug.Log("next lvl");
    }


    public void EndGame()
    {
        Debug.Log("Lost");
        Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}