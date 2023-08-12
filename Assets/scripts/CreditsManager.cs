using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{

    public void HandleBack()
    {
        SceneManager.LoadScene("__Menu");
    }
}
