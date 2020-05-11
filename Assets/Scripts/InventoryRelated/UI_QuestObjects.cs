using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_QuestObjects : MonoBehaviour
{

    [SerializeField] CanvasGroup m_canvasLevier;

    public string m_nameObject;

    public static bool m_hasLevier;

    public void UI_ShowObject()
    {
        switch (m_nameObject)
        {
            default:
            case "m_Levier":
                m_canvasLevier.alpha = 1f;
                m_hasLevier = true;
                break;
        }
    }

}
