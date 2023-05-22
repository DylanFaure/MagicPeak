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

    public void ChangeCharacter(string name, int rarity, string type)
    {
        characterName = name;
        characterRarity = rarity;
        characterType = type;
        abilities.Clear();
        abilities = new List<Ability>();

        if (characterType == "Fire")
        {
            Ability tempAbilityOne = Resources.Load<Ability>("Abilities/Fire/Fireball");
            abilities.Add(tempAbilityOne);
            if (rarity >= 4)
            {
                Ability tempAbilityTwo = Resources.Load<Ability>("Abilities/Fire/Fireblast");
                abilities.Add(tempAbilityTwo);

            }
            if (rarity >= 5)
            {
                Ability tempAbilityThree = Resources.Load<Ability>("Abilities/Fire/Firedome");
                abilities.Add(tempAbilityThree);
            }
        }
        else if (characterType == "IceWater")
        {
            Ability tempAbilityOne = Resources.Load<Ability>("Abilities/IceWater/WaterBlast");
            abilities.Add(tempAbilityOne);
            if (rarity >= 4)
            {
                Ability tempAbilityTwo = Resources.Load<Ability>("Abilities/IceWater/IceFloor");
                abilities.Add(tempAbilityTwo);

            }
            if (rarity >= 5)
            {
                Ability tempAbilityThree = Resources.Load<Ability>("Abilities/IceWater/IceDrops");
                abilities.Add(tempAbilityThree);
            }
        }
        else if (characterType == "Electric")
        {
            Ability tempAbilityOne = Resources.Load<Ability>("Abilities/Electricity/ElectricMiniPulse");
            abilities.Add(tempAbilityOne);
            if (rarity >= 4)
            {
                Ability tempAbilityTwo = Resources.Load<Ability>("Abilities/Electricity/ElectricStrike");
                abilities.Add(tempAbilityTwo);
            }
            if (rarity >= 5)
            {
                Ability tempAbilityThree = Resources.Load<Ability>("Abilities/Electricity/ElectricBeam");
                abilities.Add(tempAbilityThree);
            }
        }
        else if (characterType == "Poison")
        {
            Ability tempAbilityOne = Resources.Load<Ability>("Abilities/Poison/PoisonBall");
            abilities.Add(tempAbilityOne);
            if (rarity >= 4)
            {
                Ability tempAbilityTwo = Resources.Load<Ability>("Abilities/Poison/PoisonWave");
                abilities.Add(tempAbilityTwo);
            }
            if (rarity >= 5)
            {
                Ability tempAbilityThree = Resources.Load<Ability>("Abilities/Poison/PoisonSmoke");
                abilities.Add(tempAbilityThree);
            }
        }
        else if (characterType == "Hybrid")
        {
            Ability tempAbilityOne = Resources.Load<Ability>("Abilities/Fire/Fireball");
            abilities.Add(tempAbilityOne);
            Ability tempAbilityTwo = Resources.Load<Ability>("Abilities/Poison/PoisonSmoke");
            abilities.Add(tempAbilityTwo);
            Ability tempAbilityThree = Resources.Load<Ability>("Abilities/Electricity/ElectricBeam");
            abilities.Add(tempAbilityThree);
        }
        Debug.Log("Character changed to " + characterName + " with rarity " + characterRarity + " and type " + characterType);
        Debug.Log("Character has " + abilities.Count + " abilities");
        isNew = true;
    }
}
