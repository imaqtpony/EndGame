using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used on Key, QuestObject andHarvestItem scripts
/// </summary>
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

    /// <summary>
    /// this function is not used anymore but I let it in case
    /// </summary>
    public void UI_ShowObject()
    {
        switch (m_nameObject)
        {
            case "m_Levier":
                m_canvasLevier.SetActive(true);
                break;
            case "m_Clef":
                m_canvasKey.SetActive(true);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// we use this function
    /// </summary>
    /// <param name="p_nameGO">name of the quest object that we harvest</param>
    public void ClickOnUIQuestObject(string p_nameGO)
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
