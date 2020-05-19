using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_QuestObjects : MonoBehaviour
{

    public GameObject m_canvasLevier;
    public GameObject m_canvasKey;

    protected string m_nameObject;

    public static bool m_activateLevier;
    public static bool m_activateKey;

    private void Awake()
    {
        m_canvasLevier.SetActive(false);
        m_activateLevier = false;
        m_activateKey = false;

    }

    public void UI_ShowObject()
    {
        switch (m_nameObject)
        {
            case "m_Levier":
                Debug.Log("SET ACTIVE");
                m_canvasLevier.SetActive(true);
                break;
            case "m_Clef":
                Debug.Log("SET ACTIVE");
                m_canvasKey.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void ClicOnUIQuestObject(string p_nameGO)
    {
        if (ActivateQuestObject.m_canUseItem || Key.m_canUseItem)
        {
            switch (p_nameGO)
            {
                case "Levier":
                    m_canvasLevier.SetActive(true);
                    m_activateLevier = true;
                    break;

                case "Clef":
                    m_canvasKey.SetActive(true);
                    m_activateKey = true;
                    break;

                default:
                    break;
            }
        }
        
    }

}
