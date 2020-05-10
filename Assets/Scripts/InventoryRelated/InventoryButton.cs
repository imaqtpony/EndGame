using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public static bool m_InventoryEnabled;
    public static bool m_toolsInventoryEnabled;
    [SerializeField] GameObject m_Inventory;
    [SerializeField] GameObject m_toolsInventory;
    [SerializeField] UI_Inventory m_uiInventory;
    [SerializeField] CanvasGroup m_menuButton;
    [SerializeField] DragDrop m_dragDrop;

    [SerializeField] AudioManager m_audioManager;

    [SerializeField] AudioSource m_audioSource;

    [SerializeField] Animator m_animatorTools;
    [SerializeField] Animator m_animatorInventory;


    public void OpenInventory()
    {
        m_InventoryEnabled = !m_InventoryEnabled;
        if (m_InventoryEnabled)
        {

            m_animatorInventory.SetTrigger("OpenInventory");
            m_audioSource.PlayOneShot(m_audioManager.m_openInventorySound);
            m_menuButton.alpha = 0.3f;

            Debug.Log("OPEN INVENTORY");
        }
        else if(!m_InventoryEnabled)
        {
            m_animatorInventory.SetTrigger("CloseInventory");

            m_menuButton.alpha = 1f;
            m_audioSource.PlayOneShot(m_audioManager.m_closeInventorySound);
            Debug.Log("CLOSE INVENTORY");

            m_uiInventory.RefreshInventoryRessources();
            m_uiInventory.RemoveItemFromCraftSlot();

        }
        
    }

    public void OpenToolsInventory()
    {

        m_toolsInventoryEnabled = !m_toolsInventoryEnabled;

        if (m_toolsInventoryEnabled)
        {
            m_animatorTools.SetTrigger("OpenTools");

        }
        else
        {
            m_animatorTools.SetTrigger("CloseTools");

            m_uiInventory.RefreshInventoryTools();
            m_dragDrop.ReplaceIndicator();

        }

        m_audioSource.PlayOneShot(m_audioManager.m_openToolsInventorySound, 8f);

    }

}
