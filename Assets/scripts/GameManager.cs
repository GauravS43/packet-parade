using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int bonusCounter;
    private GameObject bonusUi1;
    private GameObject bonusUi2;
    private Animator lvlComplete;
    private bool isPlayerMoving;
    private bool gameStart;
    private PlayerMovement playerMove;
    private Animator pauseMenu;

    public void Start()
    {
        bonusUi1 = GameObject.Find("Canvas/LvlComplete/BonusUI1");
        bonusUi2 = GameObject.Find("Canvas/LvlComplete/BonusUI2");
        lvlComplete = GameObject.Find("Canvas/LvlComplete").GetComponent<Animator>();
        playerMove = GameObject.Find("Player Controller/Player").GetComponent<PlayerMovement>();
        pauseMenu = GameObject.Find("Canvas/PauseMenu").GetComponent<Animator>();
    }

    public void Update()
    {
        if (!gameStart && Input.GetButtonDown("Jump"))
        {
            GameObject.Find("Canvas/LvlTitle").GetComponent<Animator>().Play("LvlTitle_Disappear");
            GameObject.Find("Canvas/Pause").GetComponent<Animator>().Play("PauseButton_Appear");
            playerMove.enabled = true;
            gameStart = true;
            isPlayerMoving = true;
        }

        if (Input.GetKey("r")){
            Restart();
        }
    }

    public void WinLevel()
    {
        string scene = SceneManager.GetActiveScene().name;
        //not called in start as it would not get updated
        bonusCounter = GameObject.Find("Flags/BonusOrb/Bonus1").GetComponent<TriggerBonus>().bonusCounter + GameObject.Find("Flags/BonusOrb2/Bonus2").GetComponent<TriggerBonus>().bonusCounter;

        if (GameControl.control.gameProgress == int.Parse(scene.Substring(4)))
        {
            GameControl.control.gameProgress += 1;
        }


        lvlComplete.Play("LvlComplete");

        switch (bonusCounter)
        {
            case 0:
                bonusUi1.SetActive(false);
                bonusUi2.SetActive(false);
                break;
            case 1:
                bonusUi2.SetActive(false);
                GameControl.control.bonusDict[scene][0] = true;
                break;
            case 2:
                bonusUi1.SetActive(false);
                GameControl.control.bonusDict[scene][1] = true;
                break;
            default:
                GameControl.control.bonusDict[scene][0] = true;
                GameControl.control.bonusDict[scene][1] = true;
                GameControl.control.bonusDict[scene][2] = true;
                break;
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void EndGame()
    {
        Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        if (gameStart && isPlayerMoving)
        {
            pauseMenu.Play("Pause_Appear");
            playerMove.enabled = false;
            isPlayerMoving = false;
        }
        else if (gameStart && !isPlayerMoving)
        {
            pauseMenu.Play("Pause_Disappear");
            playerMove.enabled = true;
            isPlayerMoving = true;
        }

    }
}