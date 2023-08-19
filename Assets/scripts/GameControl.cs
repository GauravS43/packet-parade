using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameControl : MonoBehaviour
{
    public static GameControl control;

    public Dictionary<string, bool[]> bonusDict = new Dictionary<string, bool[]>();
    public int gameProgress = 1;
    public float musicVolume = 1;
    public float sfxVolume = 1;

    public AudioSource music;
    private AudioSource uiSFX; 


    public void playSFX()
    {
        uiSFX.volume = sfxVolume;
        uiSFX.Play();
    }

    private void Awake()
    {
        music = GameObject.Find("GameControl").GetComponent<AudioSource>();
        uiSFX = GameObject.Find("GameControl/SFXAudio").GetComponent<AudioSource>();

        if (!music.isPlaying)
        {
            music.Play();
        }

        //stores if the player has obtained the bonuses in a level
        for(int i = 1; i < 17; i++)
        {
            string sceneName = (i < 10) ? "Lvl_0" + i : "Lvl_" + i;
            bonusDict.Add(sceneName, new bool[3]);
        }

        //adds keys for bonus lvls
        for(int i=1; i<6; i++)
        {
            string sceneName = "Lvl_9" + i;
            bonusDict.Add(sceneName, new bool[3]);
        }

        //singleton pattern so we don't accidentally create multiple instances 
        DontDestroyOnLoad(gameObject);
        if (control == null)
        {
            control = this;
        }


        if (PlayerPrefs.HasKey("gameProgress"))
        {
            gameProgress = PlayerPrefs.GetInt("gameProgress");
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");

            //translates obtained stars for regular lvls
            for (int i=1; i<17; i++)
            {
                string sceneName = (i < 10) ? "Lvl_0" + i : "Lvl_" + i;
                bool[] boolArr = new bool[3];
                //translates binary rep into 3 bools
                for (int j=0; j<3; j++)
                {
                    boolArr[j] = (PlayerPrefs.GetString(sceneName)[j] == '1');
                }
                bonusDict[sceneName] = boolArr;
            }

            //translates obtained stars for bonus lvls
            for (int i = 1; i < 6; i++)
            {
                string sceneName = "Lvl_9" + i;
                bool[] boolArr = new bool[3];
                for (int j = 0; j < 3; j++)
                {
                    boolArr[j] = (PlayerPrefs.GetString(sceneName)[j] == '1');
                }
                bonusDict[sceneName] = boolArr;
            }
        }
        else
        {
            PlayerPrefs.SetInt("gameProgress", 1);
            PlayerPrefs.SetFloat("musicVolume", 1f);
            PlayerPrefs.SetFloat("sfxVolume", 1f);

            for (int i = 1; i < 17; i++)
            {
                string sceneName = (i < 10) ? "Lvl_0" + i : "Lvl_" + i;
                //Binary representation of bool[3]
                PlayerPrefs.SetString(sceneName, "000");
            }

            for (int i = 1; i < 6; i++)
            {
                string sceneName = "Lvl_9" + i;
                PlayerPrefs.SetString(sceneName, "000");
            }
        }

    }
}
