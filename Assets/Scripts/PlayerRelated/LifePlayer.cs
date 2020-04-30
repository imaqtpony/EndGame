﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GD2Lib;
using UnityEngine.UI;
public class LifePlayer : MonoBehaviour
{
    public IntVar m_lifeValue;

    [SerializeField] Collider collider;

    private Inventory inventory;
    [SerializeField] UI_Inventory m_uiInventory;

    public float m_invDuration;

    [SerializeField] Image m_backgroundLife;

    [SerializeField] private Transform lifeHeartContainer;
    [SerializeField] private Transform lifeHeartSprite;

    private int x = 0;
    private int y = 0;

    private void Start()
    {

        InstantiateHearts();
        UpdateWidthBackgroundLife();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(InvFrame());
            m_lifeValue.Value -= 1;
            x--;

            //on recupere le dernier coueur
            GameObject lastHeart = lifeHeartContainer.transform.GetChild(m_lifeValue.Value).gameObject;

            //et on le detruit
            Destroy(lastHeart);
            UpdateWidthBackgroundLife();

            if (m_lifeValue.Value == 0)
            {
                m_uiInventory.DropAllItemFunction();
                //SceneManager.LoadScene("Scene_Alex");
            }
        }
    }

    public void InstantiateHearts()
    {
        

        int SPACE_BETWEEN_HEARTS = 70;

        for (int i = 0; i < m_lifeValue.Value - 1; i++)
        {
            x++;
            RectTransform itemSlotRectTransform = Instantiate(lifeHeartSprite, lifeHeartContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.anchoredPosition = new Vector2(x * SPACE_BETWEEN_HEARTS, y * SPACE_BETWEEN_HEARTS);

        }
    }

    public void HealingFunc()
    {
        Debug.Log("on recup de la vie");
        m_lifeValue.Value += 1;
        x++;

        int SPACE_BETWEEN_HEARTS = 70;
        RectTransform itemSlotRectTransform = Instantiate(lifeHeartSprite, lifeHeartContainer).GetComponent<RectTransform>();
        itemSlotRectTransform.anchoredPosition = new Vector2(x * SPACE_BETWEEN_HEARTS, y * SPACE_BETWEEN_HEARTS);
        UpdateWidthBackgroundLife();
    }

    private IEnumerator InvFrame()
    {
        int temp = 0;
        collider.enabled = false;
        while (temp < m_invDuration)
        {
            yield return new WaitForSeconds(m_invDuration);
            temp++;
        }
        collider.enabled = true;
    }

    public void UpdateWidthBackgroundLife()
    {
        m_backgroundLife.rectTransform.sizeDelta = new Vector2(m_lifeValue.Value * 162, 162); 
    }

    

}