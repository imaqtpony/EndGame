//Last Edited : 17/05

using System;
using UnityEngine;
using UnityEngine.UI;
using GD2Lib;
using System.Collections;

/// <summary>
/// Attach this script to any GameObject. Handles the event onBoardChange for both the camera and the player
/// </summary>
public class BoardManager : MonoBehaviour
{

    [Tooltip("Array containing all of the boards data")]
    public BoardData[,] m_boardArray;

    public BoardData m_currentBoard;

    [SerializeField]
    public GD2Lib.Event m_boardChangeEvent;

    private int m_nbBoards;

    private Vector3 m_cameraOffset;

    [SerializeField]
    private GameObject m_player;

    private SpawnPlayer m_spawnScript;


    /// <summary>
    /// Sets up the array containing the boards data, place camera/player on the starting board
    /// </summary>
    private void InitializeBoardArray()
    {
        //initialize array
        for (int i = 0; i < m_nbBoards; i++)
        {
            for (int j = 0; j < m_nbBoards; j++)
            {
                GameObject go = GameObject.Find($"Board_{i}/{j}");
                if (go != null)
                {
                    m_boardArray[i, j].centerPos = go.transform.position;
                    m_boardArray[i, j].indexX = i;
                    m_boardArray[i, j].indexY = j;
                }

            }
        }

        int startingBoard = 2;

        //begin the game at 2/1 
        m_boardArray[startingBoard, startingBoard - 1].isActive = true;
        m_currentBoard = m_boardArray[startingBoard, startingBoard - 1];

        m_cameraOffset = Camera.main.transform.position - m_currentBoard.centerPos;

        Camera.main.transform.position = new Vector3(m_boardArray[startingBoard, startingBoard - 1].centerPos.x,
            m_boardArray[startingBoard, startingBoard - 1].centerPos.y + m_cameraOffset.y,
            m_boardArray[startingBoard, startingBoard - 1].centerPos.z + m_cameraOffset.z);

    }

    private void OnEnable()
    {
        m_nbBoards = 7;

        m_boardArray = new BoardData[m_nbBoards, m_nbBoards];

        m_spawnScript = m_player.GetComponent<SpawnPlayer>();

        InitializeBoardArray();

        if (m_boardChangeEvent != null)
            m_boardChangeEvent.Register(HandleBoardChange);
    }

    private void OnDisable()
    {
        StartCameraPos();
        if (m_boardChangeEvent != null)
            m_boardChangeEvent.Unregister(HandleBoardChange);
    }

    private void HandleBoardChange(GD2Lib.Event p_event, object[] p_params)
    {
        if (GD2Lib.Event.TryParseArgs(out string borderName, p_params))
        {

            MoveToOtherBoard(borderName);

        }
        else
        {
            Debug.LogError("Invalid type of argument !");
        }
    }

    /// <summary>
    /// board_x/y
    /// x = length / longueur  // y = height / hauteur (z techniquement)
    /// </summary>
    /// <param name="playerPos"> the player pos when the collision happens </param>
    /// <param name="borderName"> the name of the border triggered </param>
    private void MoveToOtherBoard(string p_borderName)
    {
        int x = m_currentBoard.indexX;
        int y = m_currentBoard.indexY;
        

        //change board depending on which border was collided
        switch (p_borderName) {

            case "LeftBorder":

                if (x - 1 >= 0)
                {
                    // change
                    SwitchBoards(x - 1, y, p_borderName);
                } else
                {
                    Debug.Log("There is nothing there");
                }

                break;
            case "RightBorder":

                if (x + 1 < m_nbBoards)
                {
                    // change
                    SwitchBoards(x + 1, y, p_borderName);
                }
                else
                {
                    Debug.Log("There is nothing there");
                }

                break;
            case "BottomBorder":

                if (y - 1 >= 0)
                {
                    // change
                    SwitchBoards(x, y - 1, p_borderName);
                }
                else
                {
                    Debug.Log("There is nothing there");
                }

                break;
            case "TopBorder":

                if (y + 1 < m_nbBoards)
                {
                    // change
                    SwitchBoards(x, y + 1, p_borderName);
                }
                else
                {
                    Debug.Log("There is nothing there");
                }

                break;

            default:
                break;
        }

    }

    private void SwitchBoards(int p_newX, int p_newY, string p_bName)
    {

        m_boardArray[m_currentBoard.indexX, m_currentBoard.indexY].isActive = false;
        m_boardArray[p_newX, p_newY].isActive = true;
        m_currentBoard = m_boardArray[p_newX, p_newY];

        // Focus the camera on the current new current board
        Camera.main.transform.position = new Vector3(m_boardArray[p_newX, p_newY].centerPos.x,
            m_boardArray[p_newX, p_newY].centerPos.y + m_cameraOffset.y,
            m_boardArray[p_newX, p_newY].centerPos.z + m_cameraOffset.z);

        //Teleport the player depending ont the border hit
        m_spawnScript.PlacePlayer(p_bName);

    }

    /// <summary>
    /// Reset camera to its starting position
    /// </summary>
    private void StartCameraPos()
    {

        m_boardArray[m_currentBoard.indexX, m_currentBoard.indexY].isActive = false;
        m_boardArray[2, 1].isActive = true;
        m_currentBoard = m_boardArray[2, 1];
        // 
        Camera.main.transform.position = new Vector3(m_boardArray[2, 1].centerPos.x,
            m_boardArray[2, 1].centerPos.y + m_cameraOffset.y,
            m_boardArray[2, 1].centerPos.z + m_cameraOffset.z);


    }

    public struct BoardData
    {
        public Vector3 centerPos;

        public int indexX, indexY;

        public bool isActive;
        
    }

}
