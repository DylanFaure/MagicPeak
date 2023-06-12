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

    public void ResetAll()
    {
        ResetCharacters();
        ResetWallet();
    }
}
