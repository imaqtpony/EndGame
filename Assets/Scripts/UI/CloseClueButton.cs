using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseClueButton : MonoBehaviour
{
    [SerializeField] GameObject m_clue;

    public void CloseClue()
    {
        if (!m_clue.activeInHierarchy)
        {
            m_clue.SetActive(true);

        }
        else
        {
            m_clue.SetActive(false);
            Invoke("DisableGotInteracted", 0.1f);
        }

    }

    public void DisableGotInteracted()
    {
        InteractWithObjects.m_gotInteracted = false;

    }

}
