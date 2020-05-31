using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// when we use the Lever or the Key, we click on their sprite on the UI
/// </summary>
public class UseQuestObject : UI_QuestObjects, IPointerDownHandler
{

    public string m_nameGO;

    //we check the name of the Sprite, if it is the Lever or the Key
    public void OnPointerDown(PointerEventData eventData)
    {
        m_nameGO = gameObject.name;
        ClickOnUIQuestObject(m_nameGO);
    }

}
