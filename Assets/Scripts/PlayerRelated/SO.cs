using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SO : ScriptableObject
{
    int m_value;

    public int Value
    {
        get { return m_value; }

        set {
            m_value = value;
            OnValueChanged(m_value);
            Debug.Log(m_value);

        }
    }

    public event Action <int> OnValueChanged;

}
