using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Winner")
        {
            //StartCoroutine(LoadCredits(3.0f));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Start Menu")
            {
                QuitGame();
            }
            else
            {
                LoadStartMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().name != "Start Menu" && SceneManager.GetActiveScene().name != "Instructions")
        {
            LoadStartMenu();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Qutting Game");
        Application.Quit();
    }

    public void LoadStartMenu()
    {
        GUIManager.resetDeathCounter();
        SceneManager.LoadScene("Start Menu");
    }
}
