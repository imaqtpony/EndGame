using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GD2Lib;
using UnityEngine.UI;

public class LifePlayer : MonoBehaviour
{
    public IntVar m_lifeValue;

    [SerializeField] Collider m_collider;

    [SerializeField] Transform m_respawnPoint;

    private Inventory m_inventory;
    [SerializeField] UI_Inventory m_uiInventory;

    public float m_invDuration;

    [SerializeField] public int m_vieMax;

    [SerializeField] Image m_backgroundLife;

    [SerializeField] private Transform m_lifeHeartContainer;
    [SerializeField] private Transform m_lifeHeartSprite;

    private int x = 0;
    private int y = 0;

    private void Start()
    {
        //m_lifeValue.Value = m_vieMax;
        InstantiateHearts();
        UpdateWidthBackgroundLife();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" && EnemyAI.m_tagCollision == "Player")
        {
            StartCoroutine(InvFrame());
            m_lifeValue.Value -= 1;
            x--;

            //on recupere le dernier coeur
            GameObject lastHeart = m_lifeHeartContainer.transform.GetChild(m_lifeValue.Value).gameObject;

            //on récupère son canvasgroup pour avoir son alpha et on le diminue a 50%
            lastHeart.GetComponent<CanvasGroup>().alpha = .5f;


            if (m_lifeValue.Value == 0)
            {
                m_uiInventory.DropAllItemFunction();
                m_uiInventory.RemoveItemFromCraftSlot();
                m_lifeValue.Value = 3;
                transform.position = m_respawnPoint.position;
            }
        }
        Debug.Log(collision.gameObject.tag);
    }

    public void InstantiateHearts()
    {
        

        int SPACE_BETWEEN_HEARTS = 70;

        for (int i = 0; i < m_vieMax - 1; i++)
        {
            x++;
            RectTransform itemSlotRectTransform = Instantiate(m_lifeHeartSprite, m_lifeHeartContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.anchoredPosition = new Vector2(x * SPACE_BETWEEN_HEARTS, y * SPACE_BETWEEN_HEARTS);

        }
    }

    public void HealingFunc()
    {
        if(m_lifeValue.Value < m_vieMax)
        {
            m_lifeValue.Value += 1;

            //on récupère le dernier coeur
            GameObject lastHeart = m_lifeHeartContainer.transform.GetChild(m_lifeValue.Value - 1).gameObject;

            //on remet son alpha a 100% quand on récupère le point de vie
            lastHeart.GetComponent<CanvasGroup>().alpha = 1f;

        }
        
    }

    /// <summary>
    /// on utilise une coroutine pour faire des frame d'invincibilié
    /// </summary>
    /// <returns>dans le while, on attend 1s avant d'incrémenter le compteur et de retablir le collider du joueur
    /// c'est a ce moment la que le joueur est invincible</returns>
    private IEnumerator InvFrame()
    {
        int temp = 0;
        m_collider.enabled = false;
        while (temp < m_invDuration)
        {
            yield return new WaitForSeconds(m_invDuration);
            temp++;
        }
        m_collider.enabled = true;
    }

    public void UpdateWidthBackgroundLife()
    {
        m_backgroundLife.rectTransform.sizeDelta = new Vector2(m_vieMax * 162, 162); 
    }

    

}
