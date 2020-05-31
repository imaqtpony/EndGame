// Last edited : 30/05

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Show 2nd part of the introduction
/// On second touch, load the world (S_Proto1)
/// Attach to the TextMeshPro Intro_2
/// </summary>
public class ShowText : MonoBehaviour
{
    private TextMeshProUGUI m_tmp;

    private bool m_isFirstTouch;

    private void Start()
    {
        //the attached text component
        m_tmp = GetComponent<TextMeshProUGUI>();
        m_tmp.alpha = 0;
        m_isFirstTouch = false;

    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            m_tmp.alpha = 1.0f;

            Touch touch = Input.GetTouch(0);

            if (m_isFirstTouch)
                SceneManager.LoadScene(2);
                //SceneManager.LoadScene(1, LoadSceneMode.Additive);


            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                m_isFirstTouch = true;

        }
    }
}
