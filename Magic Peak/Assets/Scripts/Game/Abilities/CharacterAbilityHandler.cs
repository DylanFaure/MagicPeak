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
        for (int i = 0; i < currentMage.abilities.Count; i++)
        {
            abilities.Add(new AbilityContainer(currentMage.abilities[i]));
        }
        currentMage.isNew = false;
    }

    private void Update()
    {
        CheckCurrentMage();
        ProcessCooldowns();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateAbility(abilities[0]);
        } else if (Input.GetKeyDown(KeyCode.E) && currentMage.characterRarity >= 4)
        {
            ActivateAbility(abilities[1]);
        } else if (Input.GetKeyDown(KeyCode.R) && currentMage.characterRarity >= 5)
        {
            ActivateAbility(abilities[2]);
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
