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
    public AudioClip m_FireTorchSound;
    public AudioClip m_FireSound;

}