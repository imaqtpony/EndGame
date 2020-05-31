using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public static bool m_InventoryEnabled;

    [Tooltip("UI INVENTORY RELATED")]
    [SerializeField] GameObject m_Inventory;
    [SerializeField] GameObject m_toolsInventory;
    [SerializeField] UI_Inventory m_uiInventory;

    [SerializeField] DragDrop m_dragDrop;

    [Tooltip("SOUND RELATED")]
    [SerializeField] AudioManager m_audioManager;
    [SerializeField] AudioSource m_audioSource;

    [Tooltip("ANIMATOR RELATED")]
    [SerializeField] Animator m_animatorTools;
    [SerializeField] Animator m_animatorInventory;


    private void Awake()
    {
        DisableInventory();
    }

    #region OPEN INVENTORY
    /// <summary>
    /// function used in the craft table script
    /// </summary>
    public void OpenInventory()
    {
        //used in others scripts to check if the inventory is opened or not
        m_InventoryEnabled = !m_InventoryEnabled;


        if (!m_Inventory.activeInHierarchy && m_toolsInventory.activeInHierarchy && UI_Inventory.m_firstToolsCrafted)
        {
            m_Inventory.SetActive(true);

            m_animatorInventory.SetTrigger("OpenInventory");
            m_audioSource.PlayOneShot(m_audioManager.m_openInventorySound);

        }
        else if (!m_Inventory.activeInHierarchy && !m_toolsInventory.activeInHierarchy && UI_Inventory.m_firstToolsCrafted)
        {
            m_Inventory.SetActive(true);

            m_animatorInventory.SetTrigger("OpenInventory");
            m_audioSource.PlayOneShot(m_audioManager.m_openInventorySound);

            m_toolsInventory.SetActive(true);

            m_animatorTools.SetTrigger("OpenTools");
        }

        else if(m_Inventory.activeInHierarchy && m_toolsInventory.activeInHierarchy && UI_Inventory.m_firstToolsCrafted)
        {
            Invoke("DisableInventory", .33f);
            m_animatorInventory.SetTrigger("CloseInventory");

            m_audioSource.PlayOneShot(m_audioManager.m_closeInventorySound);

            Invoke("DisableToolsInventory", .33f);

            m_animatorTools.SetTrigger("CloseTools");

        }
        else if (m_Inventory.activeInHierarchy && !m_toolsInventory.activeInHierarchy && UI_Inventory.m_firstToolsCrafted)
        {
            Invoke("DisableInventory", .33f);
            m_animatorInventory.SetTrigger("CloseInventory");

            m_audioSource.PlayOneShot(m_audioManager.m_closeInventorySound);

        }
        if (!UI_Inventory.m_firstToolsCrafted)
        {
            if (!m_Inventory.activeInHierarchy)
            {
                m_Inventory.SetActive(true);

                m_animatorInventory.SetTrigger("OpenInventory");
                m_audioSource.PlayOneShot(m_audioManager.m_openInventorySound);
            }

            else if (m_Inventory.activeInHierarchy)
            {
                Invoke("DisableInventory", .33f);
                m_animatorInventory.SetTrigger("CloseInventory");

                m_audioSource.PlayOneShot(m_audioManager.m_closeInventorySound);

            }
        }
        
    }

    #endregion

    #region OPEN TOOLS INVENTORY

    /// <summary>
    /// tools window
    /// </summary>
    public void OpenToolsInventory()
    {

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
    #endregion

    private void DisableInventory()
    {
        m_Inventory.SetActive(false);
    }

    private void DisableToolsInventory()
    {
        m_toolsInventory.SetActive(false);
    }
}
