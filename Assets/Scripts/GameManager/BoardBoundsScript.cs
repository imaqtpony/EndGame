//Last Edited : 30/05

using UnityEngine;
using GD2Lib;

/// <summary>
/// Attach this script to one of the 4 invisible borders GO 
/// They are children of the main camera
/// </summary>
public class BoardBoundsScript : MonoBehaviour
{
    [SerializeField]
    public GD2Lib.Event m_boardChangeEvent;

    private void OnTriggerEnter(Collider p_other)
    {
        if(p_other.gameObject.tag == "Player" && !p_other.isTrigger)
        {
            // raise the name to know which border was hit
            m_boardChangeEvent.Raise(gameObject.name);
        }

    }

}
