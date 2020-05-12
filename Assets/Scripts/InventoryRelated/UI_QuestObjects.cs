﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_QuestObjects : MonoBehaviour
{

    public GameObject m_canvasLevier;

    public string m_nameObject;

    public static bool m_activateLevier;

    public void UI_ShowObject()
    {
        switch (m_nameObject)
        {
            default:
            case "m_Levier":
                m_canvasLevier.SetActive(true);
                break;
        }
    }

    public void UseShowObject(string p_nameGO)
    {
        if (ActivateQuestObject.m_canUseItem)
        {
            switch (gameObject.name)
            {
                default:
                case "Levier":
                    m_canvasLevier.SetActive(false);
                    m_activateLevier = true;
                    break;
            }
        }
        
    }

}