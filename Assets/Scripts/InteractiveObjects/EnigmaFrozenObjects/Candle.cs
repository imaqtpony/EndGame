using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour, IFireReact
{
    [SerializeField]
    private GD2Lib.Event m_onLightenCandles;

    [SerializeField]
    [Tooltip("assign the order for solving this enigma")]
    private int m_order;

    private ParticleSystem m_pSys;

    private void OnEnable()
    {
        m_pSys = GetComponent<ParticleSystem>();
    }

    // Second security from onBlazeTorch event ?
    private void OnTriggerEnter(Collider p_other)
    {
        if(p_other.gameObject.tag == "Torch")
        {
            OnFire();
        }
    }


    #region IFire Interface

    public void OnFire()
    {
        if (m_pSys.isStopped)
        {
            m_pSys.Play();

            // s'inscrire dans le manager
            m_onLightenCandles.Raise(m_order, m_pSys);
        }
            
    }

    public void OnKillFire()
    {
        m_pSys.Stop();
    }

    #endregion
}
