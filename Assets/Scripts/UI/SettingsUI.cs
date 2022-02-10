using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    //reduced Singleton
    public static SettingsUI Instance;

    //sliders for volume control
    public Slider sfxSlider;
    public Slider musicSlider;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    //set slider positions to audio manager volume values
    private void Start()
    {
        sfxSlider.value = AudioManager.Instance.sfxVolume;
        musicSlider.value = AudioManager.Instance.musicVolume;
    }

    public void ChangeMusicVolume(float vol)
    {
        AudioManager.Instance.ChangeMusicVolume(vol);
    }
    
    public void ChangeSfxVolume(float vol)
    {
        AudioManager.Instance.ChangeSfxVolume(vol);
    }
}
