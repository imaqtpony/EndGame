
using UnityEngine;
using GD2Lib;
using System;
using System.Collections;

/// <summary>
/// Attach to a plant gameObject to play its animation when the player cuts it with the axe
/// If its a tree it will fall on its X axis (right)
/// Child of EnvironementObject
/// </summary>
public class Plant : EnvironementObject, IFireReact
{

    [SerializeField]
    private GD2Lib.Event m_onCutWithAxe;

    [SerializeField]
    private GD2Lib.Event m_onUseTorch;

    public QuestManager m_questManager;
    public QuestSystem m_questSystem;

    private bool m_burnThings;
    private bool m_cutThePlant;

    private Item m_attachedItem;

    [SerializeField] Item.ItemType m_itemType;

    [SerializeField] AudioManager m_audioManager;

    [SerializeField]
    private ParticleSystem m_fireVFX;


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
        if(other.gameObject.tag == "Axe" || other.gameObject.tag == "StoneAxe" && m_cutThePlant)
        {
            Debug.Log("Bye bye plant");
            // plant anim here
            //m_thisAnim.play();
            DropMaterialOnDeath(false, 0f, 1, m_itemType);
        }

        if(other.gameObject.tag == "Torch" && m_burnThings)
        {
            Debug.Log("burning af");
            OnFire();

        }
    }


    #region INTERFACE

    public void OnFire()
    {
        ParticleSystem pSys = GetComponent<ParticleSystem>();
        pSys.Play();
        m_fireVFX.Play();

        DropMaterialOnDeath(true, 2.0f, 0, 0);
        m_questManager.m_destroyPlantDone = true;
        if (!m_questManager.m_findDungeonDone)
        {
            m_questSystem.ChangeQuest("Trouvez le donjon");

            m_questManager.m_findDungeonDone = true;
        }


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
