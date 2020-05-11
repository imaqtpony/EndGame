using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTable : MonoBehaviour
{
    [SerializeField] GameObject m_craftcanvas;
    [SerializeField] GameObject m_craftText;

    [SerializeField] GameObject m_player;

    private float m_distanceWithPlayer;

    private void Awake()
    {
        m_craftcanvas.SetActive(false);

    }

    public void Update()
    {
        m_distanceWithPlayer = Vector3.Distance(m_player.transform.position, transform.position);

        if (m_distanceWithPlayer <= 2.0f)
        {
            m_craftcanvas.SetActive(true);
            m_craftText.SetActive(false);
        }
        else
        {
            m_craftcanvas.SetActive(false);
            m_craftText.SetActive(true);
        }
    }

}
