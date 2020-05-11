using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UseQuestObject : UI_QuestObjects, IPointerDownHandler
{

    public string m_nameGO;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_nameGO = gameObject.name;
        UseShowObject(m_nameGO);
    }

}
