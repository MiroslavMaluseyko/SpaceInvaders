using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Singleton pattern fo all managers
    public static AudioManager Instance;
    
    //all sounds that are using in game
    public Sound[] sounds;

    //current volume of SFX and music
    [Range(0,1)]
    public float sfxVolume;
    [Range(0,1)]
    public float musicVolume;

    //sounds that plays now to optimize volume changing
    private static List<Sound> playingNow = new List<Sound>();
    
    private void Awake()
    {
        //destroy unwanted copies for Singleton
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AudioManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

        //adding audio sources to manager
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        //load volume settings
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
        
        //start playing main theme
        Play("Music");
    }

    //method what plays sound by name
    public void Play(string name)
    {
        //find sound by name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        //check sound for null
        if (s == null)
        {
            Debug.Log("sound is null");
            return;
        }
        
        //set volume to source 
        if (s.isSfx) 
            s.source.volume = sfxVolume;
        else
            s.source.volume = musicVolume;
        s.source.Play();
        playingNow.Add(s);
    }

    
    //save new volume settings and apply it to all SFX sounds 
    public void ChangeSfxVolume(float vol)
    {
        PlayerPrefs.SetFloat("SfxVolume", vol);
        sfxVolume = vol;
        foreach (var sound in playingNow.Where(sound => sound.isSfx))
        {
            sound.source.volume = sfxVolume;
        }
    }
    
    //save new volume settings and apply it to all music sounds
    public void ChangeMusicVolume(float vol)
    {
        PlayerPrefs.SetFloat("MusicVolume", vol);
        musicVolume = vol;
        foreach (var sound in playingNow.Where(sound => !sound.isSfx))
        {
            sound.source.volume = musicVolume;
        }
    }
    
}
