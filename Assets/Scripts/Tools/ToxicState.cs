using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicState : MonoBehaviour
{
    //timer ? au bout d'un moment si le joueur n'est plus sur ce board on lance les effets visuels et spawn le portail 
    // il faut recuperer le board courant pour faire que les ennemis ne vont vers le joueur que s'il est sur le board
    // permettra aussi de savoir quand devoiler la pollution

    // une sync var pour le decompte des obj pollués? 

    [SerializeField]
    private GameObject m_enemiesSpawner;

    private float m_timerBeforeToxicState = 0.0f;

    private float m_spawningTime = 120f;

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
                //ChangeMaterial();
                Debug.Log("POLLUTION"); 

            }

        }
    }

    private void Polluted()
    {

        // spawn/ lance l'effet visuel
        GameObject thisSpawner = Instantiate(m_enemiesSpawner, gameObject.transform);
        // set as child so whenever the player picks the item it destroys the spawner as well
        thisSpawner.transform.parent = gameObject.transform;
        m_polluted = true;

    }

    private void ChangeMaterial()
    {
        m_renderer.material.shader = m_toxicShader;
    }


}


// 1) on laisse comme ça on active les ennemis que si le joueur rentre sur le board ou ils se trouvent
// ça veut dire passer le current board (recuperer le board du joueur) en parametre, et faire en sorte que l'outil sache dans quel board il se trouve
//
// 2) on instancie le board ou on les active/desactive a chaque fois