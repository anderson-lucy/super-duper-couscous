using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    [Header("UI Text")]
    public Text levelCounter;
    public Text enemyCounter;
    public int totalEnemies;
    public Text deathCounter;
    public Text winText;

    private int currentLevel;
    private static int totalDeaths = 0;
    private static GUIManager instance;

    void Start()
    {
        instance = this;
        enemyCounter.text = totalEnemies.ToString();
        deathCounter.text = totalDeaths.ToString();
        winText.enabled = false;

        currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        levelCounter.text = "Level " + currentLevel;
    }


    public void LoadStartMenu()
    {
        totalDeaths = 0;
        SceneManager.LoadScene("Start Menu");
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void resetDeathCounter()
    {
        instance._resetDeathCounter();
    }

    private void _resetDeathCounter()
    {
        totalDeaths = 0;
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

    IEnumerator LevelUp()
    {
        //display the level cleared text on the screen!
        winText.enabled = true;
        //wait for a few seconds
        yield return new WaitForSeconds(2.0f);
        winText.enabled = false;
        //move to the next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}