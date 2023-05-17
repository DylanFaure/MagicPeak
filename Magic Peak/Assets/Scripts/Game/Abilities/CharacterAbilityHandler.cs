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
    [SerializeField] Ability startingAbility;
    private AbilityContainer currentAbility;

    private void Start()
    {
        currentAbility = new AbilityContainer(startingAbility);
    }

    private void Update()
    {
        ProcessCooldowns();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateAbility(currentAbility);
        }
    }

    private void ProcessCooldowns()
    {
        currentAbility.ReduceCooldown(Time.deltaTime);
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
