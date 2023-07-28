using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlSelectManager : MonoBehaviour
{
    public void SelectLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
