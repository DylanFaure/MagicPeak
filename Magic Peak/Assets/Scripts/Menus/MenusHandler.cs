using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusHandler : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject keybindsMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        keybindsMenu.SetActive(false);
    }

    public void OpenMainMenu()
    {
        settingsMenu.SetActive(false);
        keybindsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenSettingsMenu()
    {
        mainMenu.SetActive(false);
        keybindsMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OpenKeybindsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        keybindsMenu.SetActive(true);
    }
}
