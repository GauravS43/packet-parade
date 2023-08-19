using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{

    public void HandleBack()
    {
        GameControl.control.playSFX();
        SceneManager.LoadScene("__Menu");
    }
}
