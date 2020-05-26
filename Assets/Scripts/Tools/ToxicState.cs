using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicState : MonoBehaviour
{

    [SerializeField]
    private GameObject m_enemiesSpawner;


    [SerializeField] GameObject m_pollutionAnim;

    private float m_timerBeforeToxicState = 0.0f;

    private float m_spawningTime = 15f;

    private bool m_polluted = false;

    [SerializeField] Shader m_toxicShader;
    Renderer m_renderer;

    //private void OnEnable()
    //{
    //    // Started polluting

    //    //Polluted();

    //}

    private void Start()
    {
        //m_renderer = transform.GetChild(1).GetComponent<Renderer>();
    }

    private void Update()
    {

        m_timerBeforeToxicState += Time.deltaTime;

        if (gameObject.CompareTag("Tools"))
        {
            //2 minutes
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


// 1) on laisse comme ça on active les ennemis que si le joueur rentre sur le board ou ils se trouvent
// ça veut dire passer le current board (recuperer le board du joueur) en parametre, et faire en sorte que l'outil sache dans quel board il se trouve
//
// 2) on instancie le board ou on les active/desactive a chaque fois