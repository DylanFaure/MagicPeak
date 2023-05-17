using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuManager : MonoBehaviour
{
    void Awake()
    {
        GameObject.Find("CharacterSelectionCanvas").GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("CharacterSelectionCanvas").GetComponent<Canvas>().enabled = !GameObject.Find("CharacterSelectionCanvas").GetComponent<Canvas>().enabled;
            // Time.timeScale = GameObject.Find("CharacterSelectionCanvas").GetComponent<Canvas>().enabled ? 0 : 1;
        }
    }
}
