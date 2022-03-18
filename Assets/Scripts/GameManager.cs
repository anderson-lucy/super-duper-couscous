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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Start Menu")
            {
                Debug.Log("Quitting Game");
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene("Start Menu");
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().name != "Start Menu" && SceneManager.GetActiveScene().name != "Instructions")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Restarting Level");
        }
    }
}
