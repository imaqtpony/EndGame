using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBoundsScript : MonoBehaviour
{
    [SerializeField]
    public GD2Lib.Event m_boardChangeEvent;
    // event?

    private void OnTriggerEnter(Collider p_other)
    {

        m_boardChangeEvent.Raise(p_other.gameObject.transform.position);

    }

}
