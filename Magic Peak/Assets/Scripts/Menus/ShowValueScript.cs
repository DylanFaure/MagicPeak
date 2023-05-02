using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowValueScript : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;

    public void ShowValue(float value)
    {
        float volume = value * 100f;
        m_Text.text = volume.ToString("F0") + "%";
    }
}
