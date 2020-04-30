using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data", order = 1)]
public class Data : ScriptableObject
{

    // les vars en public

    [Tooltip("is the player currently swiping")]
    public bool m_isSwiping;

    [SerializeField]
    [Tooltip("The player gameobject")]
    public GameObject m_player;


}