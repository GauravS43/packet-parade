using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl control;

    public Dictionary<string, bool[]> bonusDict = new Dictionary<string, bool[]>();
    public int gameProgress = 1;

    private void Awake()
    {
        //stores if the player has obtained the bonuses in a level
        for(int i = 1; i < 17; i++)
        {
            string sceneName = (i < 10) ? "Lvl_0" + i : "Lvl_" + i;
            bonusDict.Add(sceneName, new bool[3]);
        }

        //singleton pattern so we don't accidentally create multiple instances 
        DontDestroyOnLoad(gameObject);
        if (control == null)
        {
            control = this;
        }
    }
}
