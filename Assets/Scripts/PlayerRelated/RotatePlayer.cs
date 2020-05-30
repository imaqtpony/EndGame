//Last Edited : 30/05

using UnityEngine;
using GD2Lib;


/// <summary>
/// Player rotation On swipe event
/// </summary>
public class RotatePlayer : MonoBehaviour
{
    [SerializeField]
    private GD2Lib.Event m_swipeToRotateEvent;

    private void HandleRotationOnSwipe(GD2Lib.Event p_event, object[] p_params)
    {

        if (GD2Lib.Event.TryParseArgs(out SwipeData sData, p_params))
        {

            MovePlayer.m_stopSwipe = false;
            Ray castPoint = Camera.main.ScreenPointToRay(sData.endPosition);
            

            if (Physics.Raycast(castPoint, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 direction = hit.point - transform.position;
                direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;

                gameObject.transform.LookAt(transform.position + direction);

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
