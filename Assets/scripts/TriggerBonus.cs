using UnityEngine;

public class TriggerBonus : MonoBehaviour
{
    private Animator bonusAnim;
    //public because gamemanager.cs needs it
    [HideInInspector] public int bonusCounter = 0;

    private void Start()
    {
        bonusAnim = GameObject.Find("Canvas/Popup").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player Controller" && gameObject.name == "Bonus1")
        {
            bonusAnim.Play("BonusGet");
            gameObject.SetActive(false);
            bonusCounter += 1;
        }

        if (other.name == "Player Controller" && gameObject.name == "Bonus2")
        {
            bonusAnim.Play("BonusGet2");
            gameObject.SetActive(false);
            bonusCounter += 2;
        }
    }
}
