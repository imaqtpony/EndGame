
using System;
using UnityEngine;
using GD2Lib;

public class BoardManager : MonoBehaviour
{

    [Tooltip("Array containing all of the boards data")]
    public BoardData[,] m_boardArray;

    [SerializeField]
    public GD2Lib.Event m_boardChangeEvent;

    private void Awake()
    {
        //temp nb boards
        int nb_boards = 3;
        m_boardArray = new BoardData[nb_boards, nb_boards];

        //initialize array
        for (int i = 0; i < nb_boards; i++)
        {
            for (int j = 0; j < nb_boards; j++)
            {
                GameObject go = GameObject.Find($"Board_{i}/{j}");
                if (go != null) { 
                    m_boardArray[i, j].centerPos = go.transform.position;
                    m_boardArray[i, j].attachedScript = go.GetComponent<BoardBoundsScript>();
                    m_boardArray[i, j].attachedScript.enabled = false;
                }

            }
        }

        //begin the game at the center of the map
        m_boardArray[1, 1].isActive = true;

    }

    private void OnEnable()
    {
        if (m_boardChangeEvent != null)
            m_boardChangeEvent.Register(HandleBoardChange);
    }

    private void OnDisable()
    {
        if (m_boardChangeEvent != null)
            m_boardChangeEvent.Unregister(HandleBoardChange);
    }

    private void HandleBoardChange(GD2Lib.Event p_event, object[] p_params)
    {
        if (GD2Lib.Event.TryParseArgs(out Vector3 playerPos, p_params))
        {
            // do stuff here
            Debug.Log("Works");
        }
        else
        {
            Debug.LogError("Invalid type of argument !");
        }
    }
}


public struct BoardData 
{
    public Vector3 centerPos;

    public BoardBoundsScript attachedScript;

    private bool _active;
    public bool isActive {
        get { return _active; }

        set {
            _active = value;
            if (value == true)
            {
                Camera.main.transform.position = new Vector3(centerPos.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
                attachedScript.enabled = value;
            } else
            {
                attachedScript.enabled = value;
            }
        }
    }
}

// 1)
// ontriggerenter :
// recup coordonnées et lancer changeboard de data
// changeboard :
// desactive ancien board et active nouveau
// data a besoin du tableau 
// attached script a besoin de data

// 2)
// ontriggerenter :
// raise(othercol.pos)
//register unregister here
// handleboardchange :
// enable disable scripts and move player
// need player here && currBoard
//BM.cs et BBS.cs ont besoin du SO boardChangeEvent
