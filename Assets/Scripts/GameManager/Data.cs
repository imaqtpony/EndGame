//Last edited : 09/05

using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data", order = 1)]
public class Data : ScriptableObject
{

    // not used currently
    [Tooltip("is the player currently swiping")]
    public bool m_isSwiping;

    [SerializeField]
    [Tooltip("The player gameobject")]
    public GameObject m_player;

}