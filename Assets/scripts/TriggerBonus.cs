using UnityEngine;

public class TriggerBonus : MonoBehaviour
{
    private Animator bonusAnim;
    private AudioSource SFX;
    [SerializeField] private AudioClip bonusSFX;

    //public because gamemanager.cs needs it
    [HideInInspector] public int bonusCounter = 0;

    private void Start()
    {
        bonusAnim = GameObject.Find("Canvas/Popup").GetComponent<Animator>();
        SFX = GameObject.Find("Player Controller/Player").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player Controller" && gameObject.name == "Bonus1")
        {
            SFX.PlayOneShot(bonusSFX, GameControl.control.sfxVolume);
            bonusAnim.Play("BonusGet");
            gameObject.SetActive(false);
            bonusCounter += 1;
        }

        if (other.name == "Player Controller" && gameObject.name == "Bonus2")
        {
            SFX.PlayOneShot(bonusSFX, GameControl.control.sfxVolume);
            bonusAnim.Play("BonusGet2");
            gameObject.SetActive(false);
            bonusCounter += 2;
        }
    }
}
