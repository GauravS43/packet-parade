using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public void Start()
    {
        GameObject.Find("Canvas/MenuScreen/OptionsMenu/MusicSlider").GetComponent<Slider>().value = GameControl.control.musicVolume;
        GameObject.Find("Canvas/MenuScreen/OptionsMenu/SFXSlider").GetComponent<Slider>().value = GameControl.control.sfxVolume;
    }

    public void HandlePlay()
    {
        GameControl.control.playSFX();
        SceneManager.LoadScene((GameControl.control.gameProgress > 1) ? "_Lvl_Select" : "_Story");
    }

    public void HandleOptions()
    {
        GameControl.control.playSFX();
        GameObject.Find("Canvas/MenuScreen").GetComponent<Animator>().Play("ToOptions");
    }

    public void HandleBack()
    {
        GameControl.control.playSFX();
        GameObject.Find("Canvas/MenuScreen").GetComponent<Animator>().Play("ToMenu");
    }

    public void HandleCredits()
    {
        GameControl.control.playSFX();
        SceneManager.LoadScene("_Credits");
    }

    public void ResetProgress()
    {
        GameControl.control.playSFX();
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
        GameControl.control.music.volume = value;
        GameControl.control.musicVolume = value;
        PlayerPrefs.SetFloat("musicVolume", value);
    }

    public void ChangeSFX(float value)
    {
        GameControl.control.sfxVolume = value;
        PlayerPrefs.SetFloat("sfxVolume", value);
    }

    public void EnterDebug()
    {
        GameControl.control.playSFX();
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
