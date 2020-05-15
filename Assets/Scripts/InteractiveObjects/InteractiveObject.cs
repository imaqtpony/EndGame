
using UnityEngine;
using GD2Lib;
using System;
using System.Collections;
using TMPro;

/// <summary>
/// Attach to a plant gameObject to play its animation when the player cuts it with the axe
/// If its a tree it will fall on its X axis (right)
/// Child of EnvironementObject
/// </summary>
public class InteractiveObject : EnvironementObject, IFireReact
{

    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_textNotification;

    [SerializeField]
    private GD2Lib.Event m_onCutWithAxe;

    [SerializeField]
    private GD2Lib.Event m_onUseTorch;

    private bool m_burnThings;
    private bool m_cutThePlant;

    private Item m_attachedItem;

    [SerializeField] GameObject m_attachedObject;

    //private animator m_thisAnim;

    //private void Awake()
    //{
    //    m_thisAnim = GetComponent<Animator>();
    //    m_attachedItem = new Item {truc machin avec les bonnes infos? randomize l'amount}
    //}

    /*private void Awake()
    {
        m_audioSource.PlayOneShot(m_audioManager.m_fireSound);
        m_audioSource.Pause();

    }*/


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
            Debug.Log("Cut !");
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
        if(other.gameObject.tag == "Masse" && m_cutThePlant)
        {
            Debug.Log("Bye bye plant");
            // plant anim here
            //m_thisAnim.play();
            DropMaterialOnDeathCrate(m_attachedObject);
        }
        else
        {
            m_notification.SetActive(true);
            m_textNotification.text = "Il me faut un outil lourd.";
        }

        if (other.gameObject.tag == "Axe" && m_cutThePlant && gameObject.name == "Tronc")
        {
            var m_animator = GetComponent<Animator>();
            m_animator.SetTrigger("Activate");
        }
        else
        {
            m_notification.SetActive(true);
            m_textNotification.text = "Une hache en fer serait plus adaptee...";
        }

        if (other.gameObject.tag == "Torch" && m_burnThings)
        {
            Debug.Log("burning af");
            OnFire();

        }
        else
        {
            m_notification.SetActive(true);
            m_textNotification.text = "Je ne peux pas le bruler.";
        }
    }


    #region INTERFACE

    public void OnFire()
    {
        Debug.Log("agzelaguze");
        // play the right pSys
        ParticleSystem pSys = GetComponent<ParticleSystem>();
        pSys.Play();
        // tweaker ce temps la si on veut donner le temps au joueur pour arreter le feu
        //if(pSys.time > 5f)
        //// m_attachedItem and time before the object is destroyed
        DropMaterialOnDeath(true, 2.0f, 0, 0);
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
