using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoDisableNotification : MonoBehaviour
{

    private float m_timer = 0;


    private void OnEnable()
    {
        m_timer = 0f;
    }

    private void Update()
    {
        m_timer += Time.deltaTime;

        if(m_timer > 4)
        {
            gameObject.SetActive(false);
        }
    }

    public void PlayAnimCursor(string p_nameAnim)
    {
        GetComponent<Animator>().SetTrigger(p_nameAnim);
    }
}
