using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("Canvas/PauseMenu/MusicSlider").GetComponent<Slider>().value = GameControl.control.musicVolume;
        GameObject.Find("Canvas/PauseMenu/SFXSlider").GetComponent<Slider>().value = GameControl.control.sfxVolume;
    }

    public void BackToLvlSelect()
    {
        GameControl.control.playSFX();
        SceneManager.LoadScene("_Lvl_Select");
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
}
