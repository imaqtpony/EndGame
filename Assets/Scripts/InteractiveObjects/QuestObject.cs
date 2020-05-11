using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestObject : UI_QuestObjects
{
    public static string m_string;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_string = $"m_{gameObject.name}".ToString();
            m_nameObject = m_string;
            Debug.Log(m_string);
            UI_ShowObject();

        }
    }

}
