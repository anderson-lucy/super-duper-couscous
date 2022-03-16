using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//THIS SCRIPT WILL BE CALLED IF THE PlAYER WHACKS ALL THE MOLES IN A LEVEL
//i.e. if total_enemies = 0
public class WinScript : MonoBehaviour
{
    public Text yourText; //fill with "Level Cleared"
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp()
    {
        //display the level cleared text on the screen!
        yourText.enabled = true;
        //wait for a few seconds
        new WaitForSeconds(2);
        //get rid of text
        yourText.enabled = false;
        //move to the next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

//add in ui element for which level we are on
//add in an escape button for during the game to take you back to the start menu
