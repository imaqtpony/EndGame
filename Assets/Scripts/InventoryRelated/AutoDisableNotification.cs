using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoDisableNotification : MonoBehaviour
{

    private float m_timer = 0;
    [SerializeField] TextMeshProUGUI m_notificationText;

    private void Start()
    {
        Debug.Log("NOTIFICATION");
    }

    private void Update()
    {
        m_timer += Time.deltaTime;

        if(m_timer > 3)
        {
            m_timer = 0f;
            gameObject.SetActive(false);
        }
    }
}
