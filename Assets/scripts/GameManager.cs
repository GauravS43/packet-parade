using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject completeLevelUI;


    public void WinLevel()
    {
        completeLevelUI.SetActive(true);
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