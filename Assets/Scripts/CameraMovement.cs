using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// follows the player
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject m_player;

    private Transform m_tplayer;


    private Vector3 dist;
    private void Awake()
    {
        m_tplayer = m_player.GetComponent<Transform>();

        dist = transform.position - m_tplayer.position;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(m_tplayer.position.x, m_tplayer.position.y + dist.y, m_tplayer.position.z + dist.z);

    }

}
