using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD2Lib;


// TouchPhase began et ended

public class RotatePlayer : MonoBehaviour
{
    [SerializeField]
    private GD2Lib.Event m_swipeToRotateEvent;


    //[SerializeField]
    //private GameObject m_player;

    //private LineRenderer m_lineRenderer;

    //private void Awake()
    //{
    //    m_lineRenderer = GetComponent<LineRenderer>();
    //}


    private void HandleRotationOnSwipe(GD2Lib.Event p_event, object[] p_params)
    {

        if (GD2Lib.Event.TryParseArgs(out SwipeData sData, p_params))
        {
            //Debug.Log("swipe!");
            // the line renderer stuff
            //Vector3[] positions = new Vector3[2];
            //positions[0] = Camera.main.ScreenToWorldPoint(new Vector3(sData.startPosition.x, sData.startPosition.y, 10));
            //positions[1] = Camera.main.ScreenToWorldPoint(new Vector3(sData.endPosition.x, sData.endPosition.y, 10));
            //m_lineRenderer.positionCount = 2;
            //m_lineRenderer.SetPositions(positions);

            MovePlayer.m_stopSwipe = false;
            Ray castPoint = Camera.main.ScreenPointToRay(sData.endPosition);

            
            //Debug.DrawRay(castPoint.origin, castPoint.direction, Color.green);

            if (Physics.Raycast(castPoint, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 direction = hit.point - transform.position;
                direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;

                gameObject.transform.LookAt(transform.position + direction);
                //gameObject.transform.LookAt(new Vector3(hit.point.x,0,hit.point.z));

            }

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
