using System;
using UnityEngine;

//Not used

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "ScriptableObjects/GameEvent")]
public class GameEvent : ScriptableObject
{
    public event Action<object> onEvent = delegate { };

    public void RegisterListener(Action<object> p_listener)
    {
        onEvent += p_listener;
    }
    public void UnregisterListener(Action<object> p_listener)
    {
        onEvent -= p_listener;
    }
    public void Raise(params object[] p_params)
    {
        
        onEvent(p_params);
    }

    //internal void RegisterListener(SwipeTest swipeTest)
    //{
    //    throw new NotImplementedException();
    //}
}

