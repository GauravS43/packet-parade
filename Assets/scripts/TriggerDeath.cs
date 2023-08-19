using UnityEngine;
using System.Collections;


public class TriggerDeath : MonoBehaviour
{
    private GameManager gameManager;
    //private AudioSource SFX;
    //[SerializeField] private AudioClip deathSFX;

    private void Start()
    {
        gameManager = GameObject.Find("Main/GameManager").GetComponent<GameManager>();
        //SFX = GameObject.Find("Player Controller/Player").GetComponent<AudioSource>();
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.EndGame();
        //StartCoroutine(Death());
    }

    //IEnumerator Death()
    //{
    //    //SFX.PlayOneShot(deathSFX, GameControl.control.sfxVolume);
    //    //yield return new WaitWhile(() => SFX.isPlaying);
    //    //gameManager.EndGame();
    //}
}
