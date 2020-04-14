using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SerInt", menuName = "GD2/SerInt", order = 1)]
public class SerInt : ScriptableObject
{
    public int m_value;

    public Action<int> onValueChange;

    public int Value
    {
        get
        {
            return m_value;
        }
        set
        {
            m_value = value;

            if (onValueChange != null)
            {
                onValueChange(m_value);

            }
        }
    }


}
