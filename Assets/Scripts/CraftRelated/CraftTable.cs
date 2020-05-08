using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTable : MonoBehaviour
{
    [SerializeField] GameObject m_craftcanvas;

    private void Awake()
    {
        m_craftcanvas.SetActive(false);

    }

    private void OnTriggerEnter(Collider enter)
    {
        if (enter.gameObject.CompareTag("Player"))
        {
            m_craftcanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider exit)
    {
        if (exit.gameObject.CompareTag("Player"))
        {
            m_craftcanvas.SetActive(false);
        }
    }
}
