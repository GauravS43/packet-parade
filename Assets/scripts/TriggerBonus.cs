using UnityEngine;

public class TriggerBonus : MonoBehaviour
{
    public Animator bonusAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player Controller" && gameObject.name == "Bonus1")
        {
            bonusAnim.Play("BonusGet");
            gameObject.SetActive(false);
        }

        if (other.name == "Player Controller" && gameObject.name == "Bonus2")
        {
            bonusAnim.Play("BonusGet2");
            gameObject.SetActive(false);
        }
    }
}
