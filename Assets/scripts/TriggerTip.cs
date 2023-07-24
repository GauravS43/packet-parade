using UnityEngine;

public class TriggerTip : MonoBehaviour
{
    public Animator tipAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player Controller")
        {
            tipAnim.Play("TipGet");
        }
    }
}
