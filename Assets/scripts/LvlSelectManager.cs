using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvlSelectManager : MonoBehaviour
{
    private GameControl control = GameControl.control;

    public void SelectLevel(string sceneName)
    {
        if (GameControl.control.gameProgress >= (sceneName[sceneName.Length - 1] - '0')){
            SceneManager.LoadScene(sceneName);
        }
    }

    public void HandleBack()
    {
        SceneManager.LoadScene("_Menu");
    }

    private void Start()
    {
        Color white = new Color(255, 255, 255, 255);

        for (int i = 1; i < control.gameProgress + 1; i++)
        {
            //makes the button look active
            GameObject.Find("Canvas/LvlSelectScreen/Levels/Level_" + i).GetComponent<Image>().color = white;

            //handles the stars that correspond to bonuses
            if (control.bonusDict["Lvl_" + i][0])
            {
                GameObject.Find("Canvas/LvlSelectScreen/Levels/Level_" + i + "/bonusStar1").SetActive(true);
            }
            if (control.bonusDict["Lvl_" + i][1])
            {
                GameObject.Find("Canvas/LvlSelectScreen/Levels/Level_" + i + "/bonusStar2").SetActive(true);
            }
            if (control.bonusDict["Lvl_" + i][2])
            {
                GameObject.Find("Canvas/LvlSelectScreen/Levels/Level_" + i + "/bonusStar1").SetActive(true);
                GameObject.Find("Canvas/LvlSelectScreen/Levels/Level_" + i + "/bonusStar2").SetActive(true);
                GameObject.Find("Canvas/LvlSelectScreen/Levels/Level_" + i + "/bonusStar3").SetActive(true);
            }
        }
    }
}
