using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mage", menuName = "PeakMage")]
public class PeakMage : ScriptableObject
{
    public string characterName;
    public int characterRarity;
    public string characterType;
    public List<Ability> abilities;
    public bool isNew = false;

    public void ChangeCharacter(string name)
    {
        if (name == "Lodrès")
        {
            name = "Lodres";
        }
        Debug.Log("Changing character to " + name);
        PeakMage newMage = Resources.Load<PeakMage>("Characters/" + name + "/" + name);
        characterName = newMage.characterName;
        characterRarity = newMage.characterRarity;
        characterType = newMage.characterType;
        abilities.Clear();
        abilities = new List<Ability>();

        for (int i = 0; i < newMage.abilities.Count; i++)
        {
            abilities.Add(newMage.abilities[i]);
        }

        Debug.Log("Character changed to " + characterName + " with rarity " + characterRarity + " and type " + characterType);
        Debug.Log("Character has " + abilities.Count + " abilities");
        isNew = true;
    }
}
