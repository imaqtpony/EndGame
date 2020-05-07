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
    private void Awake()
    {

        m_Inventory.SetActive(true);
        m_audioManager.m_audioSource = GetComponent<AudioSource>();

    }

    private void Start()
    {
        m_Inventory.SetActive(false);

    }

    public void OpenInventory()
    {
        m_InventoryEnabled = !m_InventoryEnabled;
        if (m_InventoryEnabled)
        {
            m_Inventory.SetActive(true);

            m_audioManager.m_audioSource.PlayOneShot(m_audioManager.m_openInventorySound);

            Time.timeScale = 0.2f;
            m_menuButton.alpha = 0.3f;
            Debug.Log("OPEN INVENTORY");
        }
        else if(!m_InventoryEnabled)
        {
            m_menuButton.alpha = 1f;

            Time.timeScale = 1f;
            m_Inventory.SetActive(false);

            m_audioManager.m_audioSource.PlayOneShot(m_audioManager.m_closeInventorySound);

            m_uiInventory.RefreshInventoryRessources();
            m_uiInventory.RemoveItemFromCraftSlot();

        }
        
    }

    public void OpenToolsInventory()
    {

        m_toolsInventoryEnabled = !m_toolsInventoryEnabled;

        if (m_toolsInventoryEnabled)
        {
            m_toolsInventory.SetActive(true);
        }
        else
        {
            m_dragDrop.ReplaceIndicator();
            m_toolsInventory.SetActive(false);
        }
        
        
    }

}
