using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    private AudioListener audioListener;
    private float defaultVolume;

    private void Start()
    {
        audioListener = FindObjectOfType<AudioListener>();
        defaultVolume = audioSource.volume;
        volumeSlider.value = defaultVolume;
    }

    public void SetVolume()
    {
        audioSource.volume = volumeSlider.value;
    }

    public void ToggleMute()
    {
        audioListener.enabled = !audioListener.enabled;
    }

    public void ResetVolume()
    {
        audioSource.volume = defaultVolume;
        volumeSlider.value = defaultVolume;
    }
}
