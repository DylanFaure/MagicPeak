using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowValueScript : MonoBehaviour
{
    private TMP_Text m_Text;

    void Start()
    {
        m_Text = GetComponent<TMP_Text>();
    }

    public void ShowValue(float value)
    {
        m_Text.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
