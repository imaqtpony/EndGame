using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// script which use delegate when we click on the object
/// </summary>
namespace InventoryNS.Utils {

    
    public class Button_UI : MonoBehaviour, IPointerClickHandler
    {

        public Action ClickFunc = null;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {

                if (ClickFunc != null) ClickFunc();
            }

        }
    }
}