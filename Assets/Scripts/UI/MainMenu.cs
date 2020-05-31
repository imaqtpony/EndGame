using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// buttons in hte menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        //load game scene
        SceneManager.LoadScene("S_Proto1");

    }
    public void QuitGame()
    {
        //qui the game
        Application.Quit();

    }

}
