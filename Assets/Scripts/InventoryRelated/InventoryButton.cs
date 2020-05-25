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


    private void Awake()
    {
        DisableInventory();
    }

    public void OpenInventory()
    {
        m_InventoryEnabled = !m_InventoryEnabled;
        if (!m_Inventory.activeInHierarchy && m_toolsInventory.activeInHierarchy && UI_Inventory.m_firstToolsCrafted)
        {
            m_Inventory.SetActive(true);

            m_animatorInventory.SetTrigger("OpenInventory");
            m_audioSource.PlayOneShot(m_audioManager.m_openInventorySound);
            m_menuButton.alpha = 0.3f;

        }
        else if (!m_Inventory.activeInHierarchy && !m_toolsInventory.activeInHierarchy && UI_Inventory.m_firstToolsCrafted)
        {
            m_Inventory.SetActive(true);

            m_animatorInventory.SetTrigger("OpenInventory");
            m_audioSource.PlayOneShot(m_audioManager.m_openInventorySound);
            m_menuButton.alpha = 0.3f;

            m_toolsInventory.SetActive(true);

            m_animatorTools.SetTrigger("OpenTools");
        }

        else if(m_Inventory.activeInHierarchy && m_toolsInventory.activeInHierarchy && UI_Inventory.m_firstToolsCrafted)
        {
            Invoke("DisableInventory", .33f);
            m_animatorInventory.SetTrigger("CloseInventory");

            m_menuButton.alpha = 1f;
            m_audioSource.PlayOneShot(m_audioManager.m_closeInventorySound);

            m_uiInventory.RefreshInventoryRessources();
            m_uiInventory.RemoveItemFromCraftSlot();
            Invoke("DisableToolsInventory", .33f);

            m_animatorTools.SetTrigger("CloseTools");

        }
        else if (m_Inventory.activeInHierarchy && !m_toolsInventory.activeInHierarchy && UI_Inventory.m_firstToolsCrafted)
        {
            Invoke("DisableInventory", .33f);
            m_animatorInventory.SetTrigger("CloseInventory");

            m_menuButton.alpha = 1f;
            m_audioSource.PlayOneShot(m_audioManager.m_closeInventorySound);

            m_uiInventory.RefreshInventoryRessources();
            m_uiInventory.RemoveItemFromCraftSlot();
        }
        if (!UI_Inventory.m_firstToolsCrafted)
        {
            if (!m_Inventory.activeInHierarchy)
            {
                m_Inventory.SetActive(true);

                m_animatorInventory.SetTrigger("OpenInventory");
                m_audioSource.PlayOneShot(m_audioManager.m_openInventorySound);
                m_menuButton.alpha = 0.3f;
            }

            else if (m_Inventory.activeInHierarchy)
            {
                Invoke("DisableInventory", .33f);
                m_animatorInventory.SetTrigger("CloseInventory");

                m_menuButton.alpha = 1f;
                m_audioSource.PlayOneShot(m_audioManager.m_closeInventorySound);

                m_uiInventory.RefreshInventoryRessources();
                m_uiInventory.RemoveItemFromCraftSlot();

            }
        }
        
    }
    public void OpenToolsInventory()
    {
        Debug.Log("TOOLS");
        m_toolsInventoryEnabled = !m_toolsInventoryEnabled;

        if (!m_toolsInventory.activeInHierarchy)
        {
            m_toolsInventory.SetActive(true);

            m_animatorTools.SetTrigger("OpenTools");

        }
        else if(m_toolsInventory.activeInHierarchy)
        {
            Invoke("DisableToolsInventory", .33f);

            m_animatorTools.SetTrigger("CloseTools");
            m_uiInventory.RefreshInventoryTools();
            m_dragDrop.ReplaceIndicator();

        }

        m_audioSource.PlayOneShot(m_audioManager.m_openToolsInventorySound, 8f);

    }

    private void DisableInventory()
    {
        m_Inventory.SetActive(false);
    }

    private void DisableToolsInventory()
    {
        m_toolsInventory.SetActive(false);
    }
}
