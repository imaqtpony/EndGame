using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject m_prefab;   

    void Start()
    {
        Instantiate(m_prefab, transform.position, transform.rotation);
    }
}
