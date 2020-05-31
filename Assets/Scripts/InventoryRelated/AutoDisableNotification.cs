using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// this script disable automaticaly the gameobject the moment he is enabled
/// </summary>
public class AutoDisableNotification : MonoBehaviour
{

    private float m_timer;
    private void OnEnable()
    {
        m_timer = 0f;
    }

    private void Update()
    {
        m_timer += Time.deltaTime;

        //we wait 4 seconds
        if(m_timer > 4)
        {

            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// the cursor of the tutorial is attached to this gameobject
    /// this function is used in other script, that's why the trigger name is a parameter
    /// </summary>
    /// <param name="p_nameAnim">name of the trigger</param>
    public void PlayAnimCursor(string p_nameAnim)
    {
        GetComponent<Animator>().SetTrigger(p_nameAnim);
    }
}
