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

    public void Activate(GameObject caster, Vector3 mousePos)
    {
        switch (abilityClass)
        {
            case AbilityClass.Projectile:
                ActivateProjectileSpell(caster, mousePos);
                break;
            case AbilityClass.Oncursor:
                ActivateOnCursorSpell(caster, mousePos);
                break;
            case AbilityClass.Selfcast:
                ActivateSelfcastSpell(caster);
                break;
            default:
                break;
        }
    }

    private void ActivateProjectileSpell(GameObject caster, Vector3 mousePos)
    {
        GameObject projectile = Instantiate(prefab, caster.transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileSpell>().damage = damage;
        projectile.GetComponent<ProjectileSpell>().range = range;
        projectile.GetComponent<ProjectileSpell>().speed = speed;
        projectile.GetComponent<ProjectileSpell>().SetTarget(mousePos);
    }

    private void ActivateOnCursorSpell(GameObject caster, Vector3 mousePos)
    {
        // check if mousePos is in ability range
        if (Vector3.Distance(caster.transform.position, mousePos) > range)
        {
            Debug.Log("Target out of range");
            return;
        }

        Debug.Log("Target in range");

        GameObject oncursor = Instantiate(prefab, mousePos, Quaternion.identity);
        oncursor.GetComponent<OnCursorSpell>().damage = damage;
        oncursor.GetComponent<OnCursorSpell>().range = range;
        oncursor.GetComponent<OnCursorSpell>().activeTime = activeTime;
        oncursor.GetComponent<OnCursorSpell>().chargeTime = chargeTime;
        oncursor.GetComponent<OnCursorSpell>().SetTarget(mousePos);
    }

    private void ActivateSelfcastSpell(GameObject caster)
    {
        prefab.GetComponent<SelfcastSpell>().activeTime = activeTime;
        Instantiate(prefab, caster.transform.position, Quaternion.identity);
    }
}
