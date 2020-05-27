﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestManager", menuName = "ScriptableObjects/QuestManager", order = 1)]
public class QuestManager : ScriptableObject
{
    public bool m_craftToolDone;
    public bool m_destroyPlantDone;
    public bool m_findDungeonDone;
    public bool m_levierEnigmaDone;
    public bool m_candleEnigmaDone;
    public bool m_ladderPlacedDone;
    public bool m_keyEnigmaDone;

    //tutos

    public bool m_tutoToolsDone;
    public bool m_tutoDecraftDone;
    public bool m_tutoLadderDone;

}