﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTable : MonoBehaviour
{
    [SerializeField] GameObject m_craftcanvas;
    [SerializeField] GameObject m_craftText;

    private void Awake()
    {
        m_craftcanvas.SetActive(false);

    }

    private void OnTriggerEnter(Collider enter)
    {
        if (enter.gameObject.CompareTag("Player"))
        {
            m_craftcanvas.SetActive(true);
            m_craftText.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider exit)
    {
        if (exit.gameObject.CompareTag("Player"))
        {
            m_craftcanvas.SetActive(false);
            m_craftText.SetActive(true);

        }
    }
}