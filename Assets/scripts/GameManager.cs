using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int bonusCounter;
    private GameObject bonusUi1;
    private GameObject bonusUi2;
    private GameObject bonusUi3;
    private Animator lvlComplete;
    private bool isPlayerMoving;
    private bool gameStart;
    private bool gameWon;
    private PlayerMovement playerMove;
    private Animator pauseMenu;

    private AudioSource SFX;
    [SerializeField] private AudioClip winSFX;
    //[SerializeField] private AudioClip deathSFX;

    public void Start()
    {
        bonusUi1 = GameObject.Find("Canvas/LvlComplete/Star1/NotObtained");
        bonusUi2 = GameObject.Find("Canvas/LvlComplete/Star2/NotObtained");
        bonusUi3 = GameObject.Find("Canvas/LvlComplete/Star3/NotObtained");
        lvlComplete = GameObject.Find("Canvas/LvlComplete").GetComponent<Animator>();
        playerMove = GameObject.Find("Player Controller/Player").GetComponent<PlayerMovement>();
        pauseMenu = GameObject.Find("Canvas/PauseMenu").GetComponent<Animator>();

        SFX = GameObject.Find("Player Controller/Player").GetComponent<AudioSource>();
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

        if (Input.GetKeyDown("r")){
            Restart();
        }

        if (Input.GetKeyDown("p"))
        {
            Pause();
        }

        if (gameWon && Input.GetButtonDown("Jump"))
        {
            NextLevel();
        }
    }

    public void WinLevel()
    {
        SFX.PlayOneShot(winSFX, GameControl.control.sfxVolume);
        gameWon = true;
        string scene = SceneManager.GetActiveScene().name;
        //not called in start as it would not get updated
        bonusCounter = GameObject.Find("Flags/BonusOrb/Bonus1").GetComponent<TriggerBonus>().bonusCounter + GameObject.Find("Flags/BonusOrb2/Bonus2").GetComponent<TriggerBonus>().bonusCounter;

        if (GameControl.control.gameProgress == int.Parse(scene.Substring(4)) && GameControl.control.gameProgress < 16)
        {
            GameControl.control.gameProgress += 1;
            PlayerPrefs.SetInt("gameProgress", GameControl.control.gameProgress);
        }


        lvlComplete.Play("LvlComplete");

        string savedBonus = PlayerPrefs.GetString(scene);

        switch (bonusCounter)
        {
            case 0:
                break;
            case 1:
                bonusUi1.SetActive(false);
                GameControl.control.bonusDict[scene][0] = true;
                PlayerPrefs.SetString(scene, "1" + savedBonus.Substring(1));
                break;
            case 2:
                bonusUi2.SetActive(false);
                GameControl.control.bonusDict[scene][1] = true;
                PlayerPrefs.SetString(scene, savedBonus[0] + "1" + savedBonus[2]);

                break;
            default:
                bonusUi1.SetActive(false);
                bonusUi2.SetActive(false);
                bonusUi3.SetActive(false);
                GameControl.control.bonusDict[scene] = new bool[3] { true, true, true };
                PlayerPrefs.SetString(scene, "111");
                break;
        }
    }

    public void NextLevel()
    {
        int nextNum = int.Parse(SceneManager.GetActiveScene().name.Substring(4)) + 1;
        if (nextNum < 17)
        {
            string nextName = (nextNum < 10) ? "Lvl_0" + nextNum : "Lvl_" + nextNum;
            SceneManager.LoadScene(nextName);
        }
        else if (nextNum == 17)
        {
            SceneManager.LoadScene("_Credits");
        }
        else
        {
            SceneManager.LoadScene("_Lvl_Select");
        }
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
        GameControl.control.playSFX();
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