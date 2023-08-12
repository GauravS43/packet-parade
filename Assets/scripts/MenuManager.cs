using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void HandlePlay()
    {
        SceneManager.LoadScene("_Lvl_Select");
    }

    public void HandleOptions()
    {
        GameObject.Find("Canvas/MenuScreen").GetComponent<Animator>().Play("ToOptions");
    }

    public void HandleBack()
    {
        GameObject.Find("Canvas/MenuScreen").GetComponent<Animator>().Play("ToMenu");
    }

    public void ResetProgress()
    {
        GameControl.control.gameProgress = 1;
        PlayerPrefs.SetInt("gameProgress", 1);

        //resets bonusStatus for regular lvls
        for (int i = 1; i < 17; i++)
        {
            string sceneName = (i < 10) ? "Lvl_0" + i : "Lvl_" + i;
            GameControl.control.bonusDict[sceneName] = new bool[3];
            PlayerPrefs.SetString(sceneName, "000");
        }

        //resets bonusStatus for bonus lvls
        for (int i = 1; i < 6; i++)
        {
            string sceneName = "Lvl_9" + i;
            GameControl.control.bonusDict[sceneName] = new bool[3];
            PlayerPrefs.SetString(sceneName, "000");
        }

    }

    public void MuteSFX()
    {

    }

    public void MuteMusic()
    {

    }

    public void EnterDebug()
    {
        //100%s the game file
        GameControl.control.gameProgress = 16;
        PlayerPrefs.SetInt("gameProgress", 16);

        for (int i = 1; i < 17; i++)
        {
            string sceneName = (i < 10) ? "Lvl_0" + i : "Lvl_" + i;
            GameControl.control.bonusDict[sceneName] = new bool[3] { true, true, true };
            PlayerPrefs.SetString(sceneName, "111");
        }

        for (int i = 1; i < 6; i++)
        {
            string sceneName = "Lvl_9" + i;
            GameControl.control.bonusDict[sceneName] = new bool[3] { true, true, true };
            PlayerPrefs.SetString(sceneName, "111");
        }
    }
}
