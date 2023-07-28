using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public void BackToLvlSelect()
    {
        SceneManager.LoadScene("_Lvl_Select");
    }
}
