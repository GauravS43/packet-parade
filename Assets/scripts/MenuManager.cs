using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void HandlePlay()
    {
        SceneManager.LoadScene("_Lvl_Select");
    }
}
