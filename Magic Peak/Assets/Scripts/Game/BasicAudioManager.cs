using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BasicAudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

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
            SetVolume();
        }
        else
        {
            masterVolume = 1f;
            musicVolume = 0.5f;
            sfxVolume = 0.5f;
            menusVolume = 0.5f;
            SetVolume();
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
            PlayerPrefs.SetFloat("MenusVolume", menusVolume);
        }
    }

    private void SetVolume()
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        audioMixer.SetFloat("MenusVolume", Mathf.Log10(menusVolume) * 20);
    }
}
