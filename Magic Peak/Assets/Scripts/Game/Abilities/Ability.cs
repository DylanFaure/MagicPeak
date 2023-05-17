using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{
    public string Name;
    public float cooldownTime;
    public float activeTime;
    public float damage;
    public float range;
    public float speed;
    public float shield;
    public float heal;
    public float chargeTime;
    public AbilityClass abilityClass;
    public GameObject prefab;

    public enum AbilityClass
    {
        Projectile,
        Oncursor,
        Selfcast
    }

    public virtual void Activate(GameObject parent)
    {
        Debug.Log("Ability activated");
    }
}
