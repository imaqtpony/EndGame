using UnityEngine;
using GD2Lib;
using System;
using System.Collections;
using TMPro;
using UnityEngine.AI;

/// <summary>
/// Attach to a plant gameObject to play its animation when the player cuts it with the axe
/// If its a tree it will fall on its X axis (right)
/// Child of EnvironementObject
/// </summary>
public class InteractiveObject : EnvironementObject, IFireReact
{

    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_textNotification;

    [SerializeField] NavMeshObstacle m_navMeshTronc;

    [SerializeField]
    private GD2Lib.Event m_onCutWithAxe;

    [SerializeField]
    private GD2Lib.Event m_onUseTorch;

    private bool m_burnThings;
    public static bool m_cutThePlant;

    [SerializeField] Item.ItemType m_attachedItem;

    [SerializeField] GameObject m_attachedObject;

    [SerializeField] AudioManager m_audioManager;

    private AudioSource m_audioSource;

    //private animator m_thisAnim;

    //private void Awake()
    //{
    //    m_thisAnim = GetComponent<Animator>();
    //    m_attachedItem = new Item {truc machin avec les bonnes infos? randomize l'amount}
    //}

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (m_onCutWithAxe != null)
            m_onCutWithAxe.Register(HandleCutThisPlant);

        if (m_onUseTorch != null)
            m_onUseTorch.Register(HandleBurnThis);
    }
    private void OnDisable()
    {
        if (m_onCutWithAxe != null)
            m_onCutWithAxe.Unregister(HandleCutThisPlant);
        
        if (m_onUseTorch != null)
            m_onUseTorch.Unregister(HandleBurnThis);
    }

    private void HandleCutThisPlant(GD2Lib.Event p_event, object[] p_params)
    {
        if (GD2Lib.Event.TryParseArgs(out bool axeCutting, out SwipeData sData, p_params))
        {
            //Debug.Log("Cut !");
            // maybe need sData otherwise c'est ciao
            m_cutThePlant = axeCutting;

        }
        else
        {
            Debug.LogError("Invalid type of argument !");
        }
    }
    
    private void HandleBurnThis(GD2Lib.Event p_event, object[] p_params)
    {
        if (GD2Lib.Event.TryParseArgs(out bool burning, p_params))
        {

            m_burnThings = burning;

        }
        else
        {
            Debug.LogError("Invalid type of argument !");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Axe" && m_cutThePlant && gameObject.name == "Tronc")
        {
            m_audioSource.PlayOneShot(m_audioManager.m_axeHitSound);

            m_audioSource.PlayOneShot(m_audioManager.m_fallingTreeSound);
            GetComponent<Animator>().SetTrigger("Activate");
            m_navMeshTronc.enabled = false;

        }
        if (other.gameObject.tag == "StoneAxe" && m_cutThePlant && gameObject.name == "Tronc")
        {
            m_notification.SetActive(true);
            m_textNotification.text = "Il me faut une hache plus solide...";
            m_textNotification.color = new Color(255, 255, 255);
            m_audioSource.PlayOneShot(m_audioManager.m_axeHitSound);


        }

        if (other.gameObject.tag == "StoneAxe" && m_cutThePlant && gameObject.CompareTag("LevierCaisse"))
        {

            m_audioSource.PlayOneShot(m_audioManager.m_destroyingCrateSound);
            m_audioSource.PlayOneShot(m_audioManager.m_axeHitSound);
            DropMaterialOnDeathCrate(m_attachedObject, m_attachedItem);


        }
        else if (other.gameObject.tag == "StoneAxe" || other.gameObject.tag == "Axe" && m_cutThePlant && gameObject.CompareTag("BasicCaisse"))
        {

            if(gameObject.name != "Tronc")
            {
                m_audioSource.PlayOneShot(m_audioManager.m_destroyingCrateSound);
                m_audioSource.PlayOneShot(m_audioManager.m_axeHitSound);
                DropMaterialOnDeath(false, .5f, 1, m_attachedItem);
                m_audioSource.PlayOneShot(m_audioManager.m_axeHitSound);

            }



        }

        else if (gameObject.name == "Caisse" && other.CompareTag("Player"))
        {
            m_notification.SetActive(true);
            m_textNotification.text = "Il me faudrait une hache pour la casser";
            m_textNotification.color = new Color(255, 255, 255);

        }

        if (other.gameObject.tag == "Torch" && m_burnThings)
        {
            
            Debug.Log("burning af");
            OnFire();
            return;

        }

    }


    #region INTERFACE

    public void OnFire()
    {
        // play the right pSys
        ParticleSystem pSys = GetComponent<ParticleSystem>();
        if(pSys != null)
        {
            pSys.Play();
            DropMaterialOnDeath(true, 2.0f, 0, 0);

        }
        // tweaker ce temps la si on veut donner le temps au joueur pour arreter le feu
        //if(pSys.time > 5f)
        //// m_attachedItem and time before the object is destroyed
        //m_audioSource.Play();


    }

    //if on envoit de l'eau on call OnKillFire
    public void OnKillFire()
    {

    }

    private IEnumerator WaitForObjectToBurn()
    {
        yield return new WaitForSeconds(5);
    }

    // we need a particle that slowly spreads in circle
    // do we need to stop the pSys before deleting the object ?
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("BURN ANOTHER ONE");
        IFireReact burnableObj = other.GetComponent<IFireReact>();
        burnableObj?.OnFire();
    }

    #endregion

}
