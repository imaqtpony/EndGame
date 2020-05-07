using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD2Lib;

public class BoardBoundsScript : MonoBehaviour
{
    [SerializeField]
    public GD2Lib.Event m_boardChangeEvent;



    private void OnTriggerEnter(Collider p_other)
    {
        if(p_other.gameObject.tag == "Player" && !p_other.isTrigger)
        {
            Debug.Log(p_other.gameObject.name);
            m_boardChangeEvent.Raise(gameObject.name);
        }

    }

}
