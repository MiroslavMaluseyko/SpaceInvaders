using System;
using UnityEngine;
using UnityEngine.Audio;

//class for our audio manager to storage every sound in game
[Serializable]
public class Sound
{
    public string name;
    
    public AudioClip clip;
    public bool isSfx;
    public bool loop;


    [HideInInspector] public AudioSource source;
}