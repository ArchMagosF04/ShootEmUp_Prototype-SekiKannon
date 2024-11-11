using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider uISlider;

    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
        SetMusicVolume(PlayerPrefs.GetFloat("SavedMusicVolume", 100));
        SetSoundVolume(PlayerPrefs.GetFloat("SavedSoundVolume", 100));
        SetUIVolume(PlayerPrefs.GetFloat("SavedUIVolume", 100));
    }

    public void SetMasterVolume(float volume)
    {
        if (volume < 1)
        {
            volume = 0.001f; 
        }

        RefreshSlider(volume, masterSlider);

        PlayerPrefs.SetFloat("SavedMasterVolume", volume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume/100) * 20);
    }

    public void SetVolumeFromMasterSlider()
    {
        SetMasterVolume(masterSlider.value);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume < 1)
        {
            volume = 0.001f;
        }

        RefreshSlider(volume, musicSlider);

        PlayerPrefs.SetFloat("SavedMusicVolume", volume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume / 100) * 20);
    }

    public void SetVolumeFromMusicSlider()
    {
        SetMusicVolume(musicSlider.value);
    }

    public void SetSoundVolume(float volume)
    {
        if (volume < 1)
        {
            volume = 0.001f;
        }

        RefreshSlider(volume, soundSlider);

        PlayerPrefs.SetFloat("SavedSoundVolume", volume);
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume / 100) * 20);
    }

    public void SetVolumeFromSoundSlider()
    {
        SetSoundVolume(soundSlider.value);
    }

    public void SetUIVolume(float volume)
    {
        if (volume < 1)
        {
            volume = 0.001f;
        }

        RefreshSlider(volume, uISlider);

        PlayerPrefs.SetFloat("SavedUIVolume", volume);
        audioMixer.SetFloat("UIVolume", Mathf.Log10(volume / 100) * 20);
    }

    public void SetVolumeFromUISlider()
    {
        SetUIVolume(uISlider.value);
    }

    public void RefreshSlider(float volume, Slider slider)
    {
        slider.value = volume;
    }
}
