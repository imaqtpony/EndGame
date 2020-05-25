using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioManager", menuName = "ScriptableObjects/AudioManager", order = 1)]
public class AudioManager : ScriptableObject
{
    public AudioSource m_audioSource;

    public AudioClip m_openInventorySound;
    public AudioClip m_closeInventorySound;
    public AudioClip m_wooshFireTorchSound;
    public AudioClip m_fireTorchSound;
    public AudioClip m_fireSound;
    public AudioClip m_openToolsInventorySound;
    public AudioClip m_craftingSound;
    public AudioClip m_craftDropItemOnSlotSound;
    public AudioClip m_clickSound;
    public AudioClip m_grassStepSound;

    public AudioClip m_caveSound;

    public AudioClip m_axeHitSound;
    public AudioClip m_deathEnemySound;
    public AudioClip m_destroyingCrateSound;
    public AudioClip m_dropLadderSound;
    public AudioClip m_fallingTreeSound;
    public AudioClip m_LockpickSound;
    public AudioClip m_musicBoxSound;
    public AudioClip m_openingChestSound;
    public AudioClip m_pickUpSound;
    public AudioClip m_platformFallingSound;
    public AudioClip m_PlayerDamageSound;
    public AudioClip m_rollingObjectSound;


}