using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public void Start()
    {
        Debug.Log("MUSICVOLUME:" + PlayerPrefs.GetFloat("musicVolume"));
        Debug.Log("SFXVOLUME:" + PlayerPrefs.GetFloat("sfxVolume"));
        GameObject.Find("Canvas/MenuScreen/OptionsMenu/MusicSlider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("musicVolume");
        GameObject.Find("Canvas/MenuScreen/OptionsMenu/SFXSlider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("sfxVolume");
    }


    public void HandlePlay()
    {
        if (GameControl.control.gameProgress > 1)
        {
            SceneManager.LoadScene("_Lvl_Select");
        }
        else
        {
            SceneManager.LoadScene("_Story");
        }

    }

    public void HandleOptions()
    {
        GameObject.Find("Canvas/MenuScreen").GetComponent<Animator>().Play("ToOptions");
    }

    public void HandleBack()
    {
        GameObject.Find("Canvas/MenuScreen").GetComponent<Animator>().Play("ToMenu");
    }

    public void HandleCredits()
    {
        SceneManager.LoadScene("_Credits");
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

    public void ChangeMusic(float value)
    {
        GameControl.control.musicVolume = value;
        PlayerPrefs.SetFloat("musicVolume", value);
        Debug.Log("MUSICVOLUME:" + PlayerPrefs.GetFloat("musicVolume"));
    }

    public void ChangeSFX(float value)
    {
        GameControl.control.sfxVolume = value;
        PlayerPrefs.SetFloat("sfxVolume", value);
        Debug.Log("SFXVOLUME:" + PlayerPrefs.GetFloat("sfxVolume"));
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
