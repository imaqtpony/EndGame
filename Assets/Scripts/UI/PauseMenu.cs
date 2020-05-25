using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    public bool m_isPaused;

    public GameObject m_pauseMenuUI;

    void OnEnable()
    {
        m_isPaused = false;
        m_pauseMenuUI.SetActive(false);
        
    }

    public void PauseResume()
    {
        Debug.Log("PAUSE");

        m_isPaused = !m_isPaused;

        if (m_isPaused)
        {
            Time.timeScale = 0f;
            m_pauseMenuUI.SetActive(true);

        }
        else
        {
            m_pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;

        }

    }

    public void Restart()
    {

        Debug.Log("RESTART");

    }

    /*public void Menu()
    {
        SceneManager.LoadScene("s_MenuChaos");
        Time.timeScale = 1f;
        CheckPointID.m_currentRespawnIndex = 0;
        ValidateTuto.m_Check1 = false;
        ValidateTuto.m_Check2 = false;
	}

    public void ShowInputs()
    {
        PauseMenuUI.SetActive(false);
        InputsImage.SetActive(true);
        m_showInputs = false;
        m_BackButtonActive = true;

	}

    public void HideInputs()
    {
        PauseMenuUI.SetActive(true);
        InputsImage.SetActive(false);
        m_showInputs = true;
        m_BackButtonActive = false;


	}*/


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT");

    }
}
