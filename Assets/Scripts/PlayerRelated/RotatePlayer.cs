using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD2Lib;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField]
    private GD2Lib.Event m_swipeToRotateEvent;


    //[SerializeField]
    //private GameObject m_player;


    private void Awake()
    {

    }


    private void HandleRotationOnSwipe(GD2Lib.Event p_event, object[] p_params)
    {

        if (GD2Lib.Event.TryParseArgs(out SwipeData sData, p_params))
        {
            Debug.Log("swipe!");
            //Vector3[] positions = new Vector3[2];
            //positions[0] = Camera.main.ScreenToWorldPoint(new Vector3(sData.startPosition.x, sData.startPosition.y, 10));
            //positions[1] = Camera.main.ScreenToWorldPoint(new Vector3(sData.endPosition.x, sData.endPosition.y, 10));
            //m_lineRenderer.positionCount = 2;
            //m_lineRenderer.SetPositions(positions);

            //m_player.transform.LookAt(sData.endPosition);
        }
        else
        {
            Debug.LogError("Invalid type of argument !");
        }

    }


    private void OnEnable()
    {
        if (m_swipeToRotateEvent != null)
            m_swipeToRotateEvent.Register(HandleRotationOnSwipe);
    }

    private void OnDisable()
    {

        if (m_swipeToRotateEvent != null)
            m_swipeToRotateEvent.Unregister(HandleRotationOnSwipe);

    }
}
