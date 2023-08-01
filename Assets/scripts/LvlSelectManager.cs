using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvlSelectManager : MonoBehaviour
{
    private GameControl control = GameControl.control;
    private Animator lvlAnimator;

    private int animateState = 0;
    private string[] levelGroup = new string[] { "1-4", "5-8", "9-12", "13-16"};

    public void SelectLevel(string sceneName)
    {
        if (GameControl.control.gameProgress >= int.Parse(sceneName.Substring(4)))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void HandleBack()
    {
        SceneManager.LoadScene("_Menu");
    }

    private void Start()
    {
        lvlAnimator = GameObject.Find("Canvas/LvlSelectScreen/Levels").GetComponent<Animator>();

        Color white = new Color(255, 255, 255, 255);

        for (int i = 1; i < control.gameProgress + 1; i++)
        {
            string location = "Canvas/LvlSelectScreen/Levels/" + levelGroup[Mathf.FloorToInt((i-1) / 4)] + "/Level_" + i;

            //makes the button look active
            GameObject.Find(location).GetComponent<Image>().color = white;

            //handles the stars that correspond to bonuses
            if (control.bonusDict["Lvl_" + i][0])
            {
                GameObject.Find(location + "/bonusStar1").SetActive(true);
            }
            if (control.bonusDict["Lvl_" + i][1])
            {
                GameObject.Find(location + "/bonusStar2").SetActive(true);
            }
            if (control.bonusDict["Lvl_" + i][2])
            {
                GameObject.Find(location + "/bonusStar1").SetActive(true);
                GameObject.Find(location + "/bonusStar2").SetActive(true);
                GameObject.Find(location + "/bonusStar3").SetActive(true);
            }
        }
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
}
