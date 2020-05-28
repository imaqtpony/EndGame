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
        
        //SceneManager.LoadScene("S_Proto1");
        SceneManager.LoadScene("S_Matt2");
        Time.timeScale = 1f;
        Debug.Log("Restart");

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT");

    }
}
