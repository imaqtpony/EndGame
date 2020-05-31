using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GD2Lib;
using UnityEngine.UI;
using UnityEngine.AI;

/// <summary>
/// manage the life of the player and all function related
/// </summary>
public class LifePlayer : MonoBehaviour
{

    [Header("UI RELATED")]
    [SerializeField] private Transform m_lifeHeartContainer;
    [SerializeField] private Transform m_lifeHeartSprite;
    [SerializeField] Image m_backgroundLife;
    [SerializeField] GameObject m_blackScreen;


    [Header("LIFE RELATED")]
    public IntVar m_lifeValue;
    public float m_invDuration;
    [SerializeField] public int m_vieMax;

    [SerializeField] Collider[] m_collider;
    [SerializeField] Transform m_respawnPoint;

    private Inventory m_inventory;
    [SerializeField] UI_Inventory m_uiInventory;

    [SerializeField] AudioManager m_audioManager;
    private AudioSource m_audioSource;

    [SerializeField] GameObject m_boardManager;

    [SerializeField] GameObject[] m_toolsOnPlayer;

    [SerializeField] CaveSoundEffect m_caveSoundEffect;

    private int X_COLUMNS = 0;
    private int Y_LINES = 0;

    private void Start()
    {
        //m_lifeValue.Value = m_vieMax;
        m_audioSource = GetComponent<AudioSource>();
        InstantiateHearts();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" && EnemyAI.m_tagCollision == "Player")
        {
            StartCoroutine(InvFrame());
            m_lifeValue.Value -= 1;
            m_audioSource.PlayOneShot(m_audioManager.m_PlayerDamageSound);
            X_COLUMNS--;

            //we get the last heart
            GameObject lastHeart = m_lifeHeartContainer.transform.GetChild(m_lifeValue.Value).gameObject;

            //we get its animator to play the animation when the player loses a heart
            lastHeart.GetComponent<Animator>().SetTrigger("DamageAnim");


            if (m_lifeValue.Value == 0)
            {
                m_collider[0].enabled = false;
                m_collider[1].enabled = false;

                StartCoroutine(TransitionDeath());


            }
        }
    }

    private IEnumerator TransitionDeath()
    {
        m_blackScreen.SetActive(true);
        yield return new WaitForSeconds(1);

        //during the black screen
        m_boardManager.GetComponent<BoardManager>().enabled = false;
        m_boardManager.GetComponent<BoardManager>().enabled = true;

        m_uiInventory.DropAllItemFunction();
        m_uiInventory.RemoveItemFromCraftSlot();

        GetComponent<NavMeshAgent>().enabled = false;
        transform.position = m_respawnPoint.position;
        GetComponent<NavMeshAgent>().enabled = true;
        StartCoroutine(ResetHearts());

        m_caveSoundEffect.DeActivateLight();

        yield return new WaitForSeconds(1);

        m_blackScreen.SetActive(false);
        m_collider[0].enabled = true;
        m_collider[1].enabled = true;

    }

    /// <summary>
    /// we reset all the hearts of the player
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResetHearts()
    {
        m_lifeValue.Value = 3;

        foreach (GameObject tools in m_toolsOnPlayer)
        {
            tools.SetActive(false);
        }

        yield return new WaitForSeconds(1);

        //as many time there are heart in the UI
        for (int i = 0; i < m_lifeValue.Value; i++)
        {
            m_lifeHeartContainer.GetChild(i).GetComponent<Animator>().SetTrigger("HealAnim");
            yield return new WaitForSeconds(.2f);

        }
        StopCoroutine(ResetHearts());
    }

    /// <summary>
    /// at the beginning of the game
    /// </summary>
    public void InstantiateHearts()
    {
        int SPACE_BETWEEN_HEARTS = 70;

        for (int i = 0; i < m_vieMax - 1; i++)
        {
            X_COLUMNS++;
            RectTransform itemSlotRectTransform = Instantiate(m_lifeHeartSprite, m_lifeHeartContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.anchoredPosition = new Vector2(X_COLUMNS * SPACE_BETWEEN_HEARTS, Y_LINES * SPACE_BETWEEN_HEARTS);

        }
    }

    /// <summary>
    /// this function is not used but we will use it in the futur (it works)
    /// </summary>
    public void HealingFunc()
    {
        if(m_lifeValue.Value < m_vieMax)
        {
            m_lifeValue.Value += 1;

            //we get the las theart
            GameObject lastHeart = m_lifeHeartContainer.transform.GetChild(m_lifeValue.Value - 1).gameObject;

            lastHeart.GetComponent<Animator>().SetTrigger("HealAnim");


        }

    }

    /// <summary>
    /// we use coroutine to make invincibility frame
    /// </summary>
    private IEnumerator InvFrame()
    {
        m_collider[0].enabled = false;
        m_collider[1].enabled = false;
        yield return new WaitForSeconds(m_invDuration);
        m_collider[0].enabled = true;
        m_collider[1].enabled = true;

    }

}
