
using UnityEngine;
using GD2Lib;
using System;

public class Axe : MonoBehaviour
{

    [SerializeField]
    private GD2Lib.Event m_onCutWithAxe;

    [SerializeField]
    private GD2Lib.Event m_onSwipe;

    private bool m_isCurrentlyCutting;

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            m_isCurrentlyCutting = false;
            transform.rotation = Quaternion.LookRotation(new Vector3(0, transform.parent.position.y, 0));

            if (gameObject.CompareTag("StoneAxe"))
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(0, -transform.parent.position.y, 0));

            }
        }
    }

    private void OnEnable()
    {

        if (m_onSwipe != null)
            m_onSwipe.Register(HandleCutOnSwipe);
    }

    private void OnDisable()
    {
        if (m_onSwipe != null)
            m_onSwipe.Unregister(HandleCutOnSwipe);
    }

    private void HandleCutOnSwipe(GD2Lib.Event p_event, object[] p_params)
    {
        if (GD2Lib.Event.TryParseArgs(out SwipeData sData, p_params))
        {
            Debug.Log("Axe is cutting stuff !");
            m_isCurrentlyCutting = true;
            m_onCutWithAxe.Raise(m_isCurrentlyCutting, sData);

            transform.rotation = RotateAxe(sData);

        }
        else
        {
            Debug.LogError("Invalid type of argument !");
        }
    }

    private Quaternion RotateAxe(SwipeData p_sData)
    {
        Quaternion rota;

        if (p_sData.endPosition.x - p_sData.startPosition.x < 0 && gameObject.CompareTag("StoneAxe"))
        {
            return rota = transform.parent.rotation * Quaternion.Euler(180, 0, 90);

        }
        else if (p_sData.endPosition.x - p_sData.startPosition.x > 0 && gameObject.CompareTag("StoneAxe"))
        {
            //Right
            return rota = transform.parent.rotation * Quaternion.Euler(180, 0, -90);
        }

        if (p_sData.endPosition.x - p_sData.startPosition.x < 0 && gameObject.CompareTag("Axe"))
        {
            //left
            return rota = transform.parent.rotation * Quaternion.Euler(0, 0, 180);

        }
        else
        {
            //Right
            return rota = transform.parent.rotation;

        }

    }

}
