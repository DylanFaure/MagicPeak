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
    [SerializeField] private Slider SFXVolumeSlider;
    [SerializeField] private Slider menusSFXVolumeSlider;

    private float beforeMute = 1f;

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;
    private float menusVolume = 1f;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume") && PlayerPrefs.HasKey("MusicVolume") && PlayerPrefs.HasKey("SFXVolume") && PlayerPrefs.HasKey("MenusVolume"))
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            menusVolume = PlayerPrefs.GetFloat("MenusVolume");
            ApplyToSliders();
            SetVolume("All");

        } else {
            masterVolume = 1f;
            musicVolume = 0.8f;
            sfxVolume = 1f;
            menusVolume = 1f;
            ApplyToSliders();
            ApplyToMixer();
        }
    }

    private void ApplyToSliders()
    {
        masterVolumeSlider.value = masterVolume;
        musicVolumeSlider.value = musicVolume;
        SFXVolumeSlider.value = sfxVolume;
        menusSFXVolumeSlider.value = menusVolume;
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

        if (menusVolume <= 0)
        {
            audioMixer.SetFloat("MenusVolume", -80f);
        } else
        {
            audioMixer.SetFloat("MenusVolume", Mathf.Log10(menusVolume) * 20);
        }
        PlayerPrefs.SetFloat("MenusVolume", menusVolume);
    }

    public void SetVolume(string which)
    {
        switch (which)
        {
            case "Master":
                masterVolume = masterVolumeSlider.value;
                break;
            case "Music":
                musicVolume = musicVolumeSlider.value;
                break;
            case "SFX":
                sfxVolume = SFXVolumeSlider.value;
                break;
            case "Menus":
                menusVolume = menusSFXVolumeSlider.value;
                break;
            case "All":
                masterVolume = masterVolumeSlider.value;
                musicVolume = musicVolumeSlider.value;
                sfxVolume = SFXVolumeSlider.value;
                menusVolume = menusSFXVolumeSlider.value;
                break;
        }
        ApplyToMixer();
    }

    public void ToggleMute()
    {
        audioMixer.SetFloat("MasterVolume", muteButton.isOn ? -80f : beforeMute);
    }
}
