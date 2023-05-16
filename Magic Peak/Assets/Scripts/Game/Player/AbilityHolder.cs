using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private Ability Ability;

    [SerializeField] private KeyCode KeyCode;

    private float cooldownTime;
    private float activeTime;

    enum AbilityState
    {
        Ready,
        Cooldown,
        Active
    }

    private AbilityState State = AbilityState.Ready;

    void Update()
    {
        switch (State)
        {
            case AbilityState.Ready:
                if (Input.GetKeyDown(KeyCode))
                {
                    Ability.Activate(gameObject);
                    State = AbilityState.Active;
                    activeTime = Ability.activeTime;
                }
                break;
            case AbilityState.Cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    State = AbilityState.Ready;
                }
                break;
            case AbilityState.Active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    State = AbilityState.Cooldown;
                    cooldownTime = Ability.cooldownTime;
                }
                break;
        }
    }
}
