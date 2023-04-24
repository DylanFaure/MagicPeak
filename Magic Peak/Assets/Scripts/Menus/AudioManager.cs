using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Toggle muteButton;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider menuVolumeSlider;


    [SerializeField] private float defaultVolume = 1f;
    private float beforeMute = 1f;

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;
    private float menuVolume = 1f;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            menuVolume = PlayerPrefs.GetFloat("MenuVolume");
            ApplyToSliders();
            SetVolume();

        } else {
            masterVolume = 1f;
            musicVolume = 0.8f;
            sfxVolume = 1f;
            menuVolume = 1f;
            ApplyToSliders();
            ApplyToMixer();
        }
    }

    private void ApplyToSliders()
    {
        masterVolumeSlider.value = masterVolume;
        musicVolumeSlider.value = musicVolume;
        sfxVolumeSlider.value = sfxVolume;
        menuVolumeSlider.value = menuVolume;
    }

    private void ApplyToMixer()
    {
        if (masterVolume <= 0)
        {
            audioMixer.SetFloat("MasterVolume", -80f);
        } else
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        }
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        if (musicVolume <= 0)
        {
            audioMixer.SetFloat("MusicVolume", -80f);
        } else
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        }
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        if (sfxVolume <= 0)
        {
            audioMixer.SetFloat("SFXVolume", -80f);
        } else
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        }
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);

        if (menuVolume <= 0)
        {
            audioMixer.SetFloat("MenuVolume", -80f);
        } else
        {
            audioMixer.SetFloat("MenuVolume", Mathf.Log10(menuVolume) * 20);
        }
        PlayerPrefs.SetFloat("MenuVolume", menuVolume);
    }

    public void SetVolume()
    {
        masterVolume = masterVolumeSlider.value;
        musicVolume = musicVolumeSlider.value;
        sfxVolume = sfxVolumeSlider.value;
        menuVolume = menuVolumeSlider.value;
        ApplyToMixer();
    }

    public void ToggleMute()
    {
        audioMixer.SetFloat("MasterVolume", muteButton.isOn ? -80f : beforeMute);
    }
}
