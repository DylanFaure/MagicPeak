using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Keybind : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonLbl;
    [SerializeField] private string keybindName;

    // Start is called before the first frame update
    void Start()
    {
        buttonLbl.text = PlayerPrefs.GetString(keybindName, "Awaiting Input");
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonLbl.text == "Awaiting Input")
        {
            foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keycode))
                {
                    buttonLbl.text = keycode.ToString();
                    PlayerPrefs.SetString(keybindName, keycode.ToString());
                    PlayerPrefs.Save();
                }
            }
        }
    }

    public void ChangeKeybind()
    {
        buttonLbl.text = "Awaiting Input";
    }
}
