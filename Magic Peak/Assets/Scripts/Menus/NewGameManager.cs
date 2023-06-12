using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    private string[] characterPrefs = {
        "Alec", "Almia", "Arianna", "Aries", "Arsenic", "Artorias", "Arum", "Baldur", "Belladonna",
        "Buld", "Burns", "Camage", "Chap", "Charon", "Creed", "Diaval", "Elaina", "Elea", "Friz",
        "Fyru", "Gally", "Gerard", "Jack", "Jaden", "Jean", "Jetchi", "Jila", "Keyneth", "Korog",
        "Lia", "Lionel", "Lodres", "Lyros", "Maui", "Misa", "Obeiron", "Praesithe", "Primo", "Rash",
        "Renal", "Rhus", "Sana", "Saten", "Spade", "Tiana", "Vega", "Viper"
    };

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("walletData"))
        {
            PlayerPrefs.SetInt("walletData", 0);
        }

        if (!PlayerPrefs.HasKey("CharacterSelected"))
        {
            PlayerPrefs.SetString("CharacterSelected", "Roger");
        }

        if (!PlayerPrefs.HasKey("currentHealth"))
        {
            PlayerPrefs.SetFloat("currentHealth", 100);
        }

        if (!PlayerPrefs.HasKey("maxHealth"))
        {
            PlayerPrefs.SetFloat("maxHealth", 100);
        }

        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetFloat("level", 1);
        }

        if (!PlayerPrefs.HasKey("experience"))
        {
            PlayerPrefs.SetFloat("experience", 0);
        }

        if (!PlayerPrefs.HasKey("experienceNeeded"))
        {
            PlayerPrefs.SetFloat("experienceNeeded", 100);
        }

        if (!PlayerPrefs.HasKey("attackDamage"))
        {
            PlayerPrefs.SetFloat("attackDamage", 15f);
        }

        foreach (string character in characterPrefs)
        {
            PlayerPrefs.SetInt(character, 0); // Définir la valeur de la PlayerPref à 0 (non débloqué)
        }

        if (!PlayerPrefs.HasKey("Roger"))
        {
            PlayerPrefs.SetInt("Roger", 1);
        }

        if (!PlayerPrefs.HasKey("Top"))
        {
            PlayerPrefs.SetString("Top", "Z");
        }
        if (!PlayerPrefs.HasKey("Bottom"))
        {
            PlayerPrefs.SetString("Bottom", "S");
        }
        if (!PlayerPrefs.HasKey("Left"))
        {
            PlayerPrefs.SetString("Left", "Q");
        }
        if (!PlayerPrefs.HasKey("Right"))
        {
            PlayerPrefs.SetString("Right", "D");
        }
        if (!PlayerPrefs.HasKey("Spell1"))
        {
            PlayerPrefs.SetString("Spell1", "Alpha1");
        }
        if (!PlayerPrefs.HasKey("Spell2"))
        {
            PlayerPrefs.SetString("Spell2", "Alpha2");
        }
        if (!PlayerPrefs.HasKey("Spell3"))
        {
            PlayerPrefs.SetString("Spell3", "Alpha3");
        }
        PlayerPrefs.Save(); // Enregistrer les modifications des PlayerPrefs
    }

    public void ResetCharacters()
    {
        foreach (string character in characterPrefs)
        {
            PlayerPrefs.SetInt(character, 0); // Définir la valeur de la PlayerPref à 0 (non débloqué)
        }
        PlayerPrefs.SetInt("Roger", 1);
        PlayerPrefs.SetString("CharacterSelected", "Roger"); // Définir le personnage sélectionné à Roger de base (débloqué)
        PlayerPrefs.Save(); // Enregistrer les modifications des PlayerPrefs
    }

    public void ResetWallet()
    {
        PlayerPrefs.SetInt("walletData", 0);
        PlayerPrefs.Save();
    }

    public void ResetPlayerStats()
    {
        PlayerPrefs.SetFloat("currentHealth", 100);
        PlayerPrefs.SetFloat("maxHealth", 100);
        PlayerPrefs.SetFloat("level", 1);
        PlayerPrefs.SetFloat("experience", 0);
        PlayerPrefs.SetFloat("experienceNeeded", 100);
        PlayerPrefs.SetFloat("attackDamage", 15f);
    }

    public void ResetAll()
    {
        ResetCharacters();
        ResetWallet();
        ResetPlayerStats();
    }
}
