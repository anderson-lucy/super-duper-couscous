using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    public Text wonText;
    public Image musicCredits;

    void Start()
    {
        StartCoroutine(ShowCredits());
    }

    void Update()
    {
        
    }

    IEnumerator ShowCredits()
    {
        wonText.enabled = true;
        musicCredits.enabled = false;
        yield return new WaitForSeconds(3.0f);
        wonText.enabled = false;
        musicCredits.enabled = true;
        yield return new WaitForSeconds(2.0f);
        GUIManager.resetDeathCounter();
        SceneManager.LoadScene("Start Menu");
    }
}
