using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private GameObject m_prefab;   

    void Awake()
    {

        m_prefab = GameObject.Find("Ennemy");
        Instantiate(m_prefab, transform.position, transform.rotation);
    }
}
