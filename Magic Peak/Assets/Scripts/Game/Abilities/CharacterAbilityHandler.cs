using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityContainer
{
    public Ability ability;
    public float currentCooldown;
    public float currentActiveTime;
    public bool isOnCooldown;
    public bool isActive;
    public bool isReady = true;

    public AbilityContainer(Ability ability)
    {
        this.ability = ability;
        currentCooldown = 0f;
        currentActiveTime = 0f;
        isOnCooldown = false;
        isActive = false;
    }

    internal void Cooldown()
    {
        currentCooldown = ability.cooldownTime;
        isOnCooldown = true;
        isReady = false;
    }

    public void ReduceCooldown(float amount)
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= amount;
        } else if (currentCooldown <= 0f)
        {
            currentCooldown = 0f;
            isOnCooldown = false;
            isReady = true;
        }
    }
}

public class CharacterAbilityHandler : MonoBehaviour
{
    [SerializeField] private PeakMage currentMage;

    private List<AbilityContainer> abilities = new List<AbilityContainer>();

    private void Awake()
    {
        if (currentMage.characterName == "Placeholder" || currentMage.characterName != PlayerPrefs.GetString("CharacterSelected"))
        {
            LoadLastMage();
        }

        for (int i = 0; i < currentMage.abilities.Count; i++)
        {
            abilities.Add(new AbilityContainer(currentMage.abilities[i]));
        }
        currentMage.isNew = false;

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
    }

    private void Update()
    {
        CheckCurrentMage();
        ProcessCooldowns();

        if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Spell1"))))
        {
            ActivateAbility(abilities[0]);
        } else if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Spell2")))
        && currentMage.characterRarity >= 4)
        {
            ActivateAbility(abilities[1]);
        } else if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Spell3")))
        && currentMage.characterRarity >= 5)
        {
            ActivateAbility(abilities[2]);
        }
    }

    private void LoadLastMage()
    {
        if (PlayerPrefs.HasKey("CharacterSelected"))
        {
            currentMage.ChangeCharacter(PlayerPrefs.GetString("CharacterSelected"));
        }
    }

    private void CheckCurrentMage()
    {
        if (currentMage.isNew)
        {
            abilities.Clear();
            for (int i = 0; i < currentMage.abilities.Count; i++)
            {
                abilities.Add(new AbilityContainer(currentMage.abilities[i]));
            }
            currentMage.isNew = false;
        }
    }

    private void ProcessCooldowns()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].ReduceCooldown(Time.deltaTime);
        }
    }

    public void ActivateAbility(AbilityContainer ability)
    {
        if (ability.currentCooldown > 0f)
        {
            Debug.Log("Ability is on cooldown");
            return;
        }
        Debug.Log("Ability activated");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        if (ability.ability.Activate(gameObject, mousePos))
        {
            ability.Cooldown();
        }
    }
}
