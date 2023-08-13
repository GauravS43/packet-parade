using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryText : MonoBehaviour
{
    private float delay = 0.05f;
    private bool skipStory = false;

    private string[] textArr = new string[]
    {
        "Every second, millions explore the internet, oblivious to the intricate ballet of network packets that power their access.",
        "These unsung heroes, each containing a tiny piece of a greater whole, journey through challenges to ensure a seamless flow of data.",
        "This is the story of a few packets, those that traversed the harshest corners of cyberspace and reached their ultimate destinaton.",
        "Press Space to Continue."
    };

    private void Start()
    {
        StartCoroutine(ShowText());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && skipStory)
        {
            SceneManager.LoadScene("Lvl_01");
        }
        else if (Input.anyKey && !skipStory)
        {
            skipStory = true;
            GameObject.Find("Canvas/Story/SkipText").SetActive(true);
        }

    }
    IEnumerator ShowText()
    {
        for (int i = 0; i < textArr.Length; i++)
        {
            //doesn't display "Press Space to Continue" if skip text is already displayed
            if (i == 3 && skipStory) continue;

            Text txtObj = GameObject.Find("Canvas/Story/Text" + i).GetComponent<Text>();
            for (int j = 0; j <= textArr[i].Length; j++)
            {
                txtObj.text = textArr[i].Substring(0, j);
                yield return new WaitForSeconds(delay);
            }
        }

        skipStory = true;
    }
}
