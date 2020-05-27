using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {

        SceneManager.LoadScene("S_Proto1");

    }
    public void QuitGame()
    {

        Application.Quit();

    }

}
