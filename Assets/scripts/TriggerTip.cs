using UnityEngine;

public class TriggerTip : MonoBehaviour
{
    private Animator tipAnim;

    private void Start()
    {
        tipAnim = GameObject.Find("Canvas/Popup").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player Controller")
        {
            tipAnim.Play("TipGet");
        }
    }
}
