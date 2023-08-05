using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvlSelectManager : MonoBehaviour
{
    private GameControl control = GameControl.control;
    private Animator lvlAnimator;

    private int animateState = 0;
    private int bonusProgress = 0;
    private string[] levelGroup = new string[] { "1-4", "5-8", "9-12", "13-16"};

    public void SelectLevel(string sceneName)
    {
        int lvlNum = int.Parse(sceneName.Substring(4));

        if (lvlNum < 90)
        {
            if (GameControl.control.gameProgress >= lvlNum)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
        else
        {
            if (bonusProgress/8 >= (lvlNum - 90))
            {
                SceneManager.LoadScene(sceneName);
            }

        }
    }

    public void HandleBack()
    {
        SceneManager.LoadScene("_Menu");
    }

    public void rightPress()
    {
        lvlAnimator.Play("LvlSwitchR" + Mathf.Abs(animateState) % 4);
        animateState++;
    }

    public void leftPress()
    {
        lvlAnimator.Play("LvlSwitchL" + Mathf.Abs(animateState) % 4);
        animateState--;
    }

    private void Start()
    {
        lvlAnimator = GameObject.Find("Canvas/LvlSelectScreen/Levels").GetComponent<Animator>();

        Color white = new Color(255, 255, 255, 255);

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

        Debug.Log(bonusProgress);

        for (int i = 8; i <= bonusProgress; i+= 8)
        {
            GameObject.Find("Canvas/LvlSelectScreen/Levels/" + levelGroup[Mathf.FloorToInt((i / 8) - 1)] + "/Level_B" + (i / 8)).GetComponent<Image>().color = white;
        }

    }
}
