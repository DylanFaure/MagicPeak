using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    private float defaultVolume;

    private void Start()
    {
        defaultVolume = audioSource.volume;
        volumeSlider.value = defaultVolume;
    }

    public void SetVolume()
    {
        audioSource.volume = volumeSlider.value;
    }

    public void ResetVolume()
    {
        audioSource.volume = defaultVolume;
        volumeSlider.value = defaultVolume;
    }
}
