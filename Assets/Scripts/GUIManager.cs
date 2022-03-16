using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public Text enemyCounter;
    public int totalEnemies;
    public Text deathCounter;

    public Text winText;

    private static int totalDeaths = 0;
    private static GUIManager instance;

    void Start()
    {
        instance = this;
        enemyCounter.text = totalEnemies.ToString();
        deathCounter.text = totalDeaths.ToString();
        winText.enabled = false;
    }

    public static void EnemyCountdown()
    {
        instance._EnemyCountdown();
    }

    private void _EnemyCountdown()
    {
        totalEnemies -= 1;
        enemyCounter.text = totalEnemies.ToString();
        if (totalEnemies <= 0)
        {
            StartCoroutine(LevelUp());
        }
    }

    public static void DeathCounting()
    {
        instance._DeathCounting();
    }

    private void _DeathCounting()
    {
        totalDeaths += 1;
        deathCounter.text = totalDeaths.ToString();
    }

    //public void LevelUp()
    //{
    //    //display the level cleared text on the screen!
    //    winText.enabled = true;
    //    //wait for a few seconds
    //    new WaitForSeconds(2.0f);
    //    //move to the next level
    //    Debug.Log("load scene");
    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    IEnumerator LevelUp()
    {
        //display the level cleared text on the screen!
        winText.enabled = true;
        //wait for a few seconds
        yield return new WaitForSeconds(2.0f);
        //move to the next level
        Debug.Log("load scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}