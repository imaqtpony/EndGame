using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// follows the player
// not used anymore (for now)
public class CameraMovement : MonoBehaviour
{
    //[SerializeField]
    //private GameObject m_player;

    //private Transform m_tplayer;

    [SerializeField]
    private GameObject board;

    private Vector3 dist;
    private void Awake()
    {
        dist = transform.position - board.transform.position;

        //transform.position = DistToBoard(dist);

        //m_tplayer = m_player.GetComponent<Transform>();

    }

    private Vector3 DistToBoard(Vector3 p_dist)
    {
        return new Vector3(board.transform.position.x, board.transform.position.y + p_dist.y, board.transform.position.z + p_dist.z);
    }

}
