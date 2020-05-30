//Last Edited : 30/05

using UnityEngine;

/// <summary>
/// Attach this script to a tool object instance when on the ground
/// After m_spawningTime, pop down the spawner and the toxic state FX
/// </summary>
public class ToxicState : MonoBehaviour
{

    [SerializeField]
    private GameObject m_enemiesSpawner;

    [SerializeField] GameObject m_pollutionAnim;

    private float m_timerBeforeToxicState = 0.0f;

    private float m_spawningTime = 15f;

    private bool m_polluted = false;

    private void Update()
    {

        m_timerBeforeToxicState += Time.deltaTime;

        if (gameObject.CompareTag("Tools"))
        {
            //Wait m_spawningTime seconds
            if (m_timerBeforeToxicState % 60f > m_spawningTime && !m_polluted)
            {
                Polluted();

            }

        }
    }

    private void Polluted()
    {

        // spawn/ lance l'effet visuel
        GameObject thisPollutedCreep = Instantiate(m_pollutionAnim);
        thisPollutedCreep.transform.parent = transform;
        thisPollutedCreep.transform.position = new Vector3(transform.position.x, 0.016f, transform.position.z);

        GameObject thisSpawner = Instantiate(m_enemiesSpawner, transform);
        // set as child so whenever the player picks the item it destroys the spawner as well
        thisSpawner.transform.parent = transform;
        m_polluted = true;

    }


}
