using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvlSelectManager : MonoBehaviour
{
    private GameControl control = GameControl.control;
    private Animator lvlAnimator;

    private Color white = new Color(255, 255, 255, 255);
    private int animateState = 4000;
    private int bonusProgress = 0;
    private string[] levelGroup = new string[] { "1-4", "5-8", "9-12", "13-16", "B5"};

    public void SelectLevel(string sceneName)
    {
        int lvlNum = int.Parse(sceneName.Substring(4));

        if (lvlNum < 90)
        {
            if (GameControl.control.gameProgress >= lvlNum)
            {
                GameControl.control.playSFX();
                SceneManager.LoadScene(sceneName);
            }
        }
        else
        {
            if (bonusProgress/8 >= (lvlNum - 90))
            {
                GameControl.control.playSFX();
                SceneManager.LoadScene(sceneName);
            }

        }
    }

    public void HandleBack()
    {
        GameControl.control.playSFX();
        SceneManager.LoadScene("__Menu");
    }

    public void rightPress()
    {
        GameControl.control.playSFX();
        lvlAnimator.Play("LvlSwitchR" + animateState % 5);
        animateState++;
    }

    public void leftPress()
    {
        GameControl.control.playSFX();
        lvlAnimator.Play("LvlSwitchL" + animateState % 5);
        animateState--;
    }

    private void Start()
    {
        lvlAnimator = GameObject.Find("Canvas/LvlSelectScreen/Levels").GetComponent<Animator>();

        //regular levels
        for (int i = 1; i < control.gameProgress + 1; i++)
        {
            string location = "Canvas/LvlSelectScreen/Levels/" + levelGroup[Mathf.FloorToInt((i-1) / 4)] + "/Level_" + i;
            string sceneName = (i < 10) ? "Lvl_0" + i : "Lvl_" + i;

            //makes the button look active
            GameObject.Find(location).GetComponent<Image>().color = white;

            //handles the stars that correspond to bonuses
            if (control.bonusDict[sceneName][0])
            {
                GameObject.Find(location + "/bonusStar1").SetActive(true);
                bonusProgress += 1;
            }
            if (control.bonusDict[sceneName][1])
            {
                GameObject.Find(location + "/bonusStar2").SetActive(true);
                bonusProgress += 1;
            }
            if (control.bonusDict[sceneName][2])
            {
                GameObject.Find(location + "/bonusStar3").SetActive(true);
            }
        }

        //bonus levels
        for (int i = 8; i <= bonusProgress; i+= 8)
        {
            string sceneName = "Lvl_9" + (i/8);
            string location = "Canvas/LvlSelectScreen/Levels/" + levelGroup[(i / 8) - 1] + "/Level_B" + (i / 8);

            GameObject.Find(location).GetComponent<Image>().color = white;

            if (control.bonusDict[sceneName][0])
            {
                GameObject.Find(location + "/bonusStar1").SetActive(true);
                bonusProgress += 1;
            }
            if (control.bonusDict[sceneName][1])
            {
                GameObject.Find(location + "/bonusStar2").SetActive(true);
                bonusProgress += 1;
            }
            if (control.bonusDict[sceneName][2])
            {
                GameObject.Find(location + "/bonusStar3").SetActive(true);
            }
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            leftPress();
        }
        if (Input.GetKeyDown("right"))
        {
            rightPress();
        }
    }
}
