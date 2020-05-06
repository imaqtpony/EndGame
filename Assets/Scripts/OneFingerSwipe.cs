// Matt

using System;
using UnityEngine;
using GD2Lib;

/// <summary>
/// Screen swipe detection handler
/// </summary>
public class OneFingerSwipe : MonoBehaviour
{
    private Vector2 m_currentFingerPos;
    private Vector2 m_initialFingerPos;

    [SerializeField]
    private GD2Lib.Event m_swipeEvent = null;

    [SerializeField]
    private Data m_data = null;

    private float m_minDistForSwipe = 15f;

    //public static event Action<SwipeData> OnSwipe = delegate { }

    private void Update()
    {

        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                m_initialFingerPos = touch.position;
                m_currentFingerPos = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                m_currentFingerPos = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended ||touch.phase == TouchPhase.Canceled)
            {
                m_currentFingerPos = touch.position;
                DetectSwipe();
            }
        }
    }

    // if it is a swipe (movement greater than m_minDistForSwipe)
    private void DetectSwipe()
    {
        if (VerticalMovementDist() > m_minDistForSwipe || HorizontalMovementDist() > m_minDistForSwipe)
        {
            m_data.m_isSwiping = true;
            ReturnSwipe();
            m_initialFingerPos = m_currentFingerPos;
        } else
        {
            m_data.m_isSwiping = false;
        }
    }

    //assign swipe data to delegate : use it in any other script as such
    private void ReturnSwipe()
    {
        SwipeData swipeData = new SwipeData()
        {
            startPosition = m_initialFingerPos,
            endPosition = m_currentFingerPos
        };

        m_swipeEvent.Raise(swipeData);
        //static delegate
        //OnSwipe(swipeData);
    }

    private float VerticalMovementDist()
    {
        return Mathf.Abs(m_currentFingerPos.y - m_initialFingerPos.y);
    }

    private float HorizontalMovementDist()
    {
        return Mathf.Abs(m_currentFingerPos.x - m_initialFingerPos.x);
    }


}

// swipe data constructor
public struct SwipeData
{
    public Vector2 startPosition;
    public Vector2 endPosition;
}

