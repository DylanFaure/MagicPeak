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
    public float castOffset;
    public AbilityClass abilityClass;
    public GameObject prefab;

    public enum AbilityClass
    {
        Projectile,
        Oncursor,
        Selfcast,
        Laser
    }

    public bool Activate(GameObject caster, Vector3 mousePos)
    {
        switch (abilityClass)
        {
            case AbilityClass.Projectile:
                return ActivateProjectileSpell(caster, mousePos);
            case AbilityClass.Oncursor:
                return ActivateOnCursorSpell(caster, mousePos);
            case AbilityClass.Selfcast:
                return ActivateSelfcastSpell(caster);
            case AbilityClass.Laser:
                return ActivateLaserSpell(caster, mousePos);
        }
        return true;
    }

    private bool ActivateProjectileSpell(GameObject caster, Vector3 mousePos)
    {
        GameObject projectile = Instantiate(prefab, caster.transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileSpell>().damage = damage;
        projectile.GetComponent<ProjectileSpell>().range = range;
        projectile.GetComponent<ProjectileSpell>().speed = speed;
        projectile.GetComponent<ProjectileSpell>().SetTarget(mousePos);
        return true;
    }

    private bool ActivateOnCursorSpell(GameObject caster, Vector3 mousePos)
    {
        // check if mousePos is in ability range
        if (Vector3.Distance(caster.transform.position, mousePos) > range)
        {
            Debug.Log("Target out of range");
            return false;
        }

        Debug.Log("Target in range");

        GameObject oncursor = Instantiate(prefab, mousePos, Quaternion.identity);
        oncursor.GetComponent<OnCursorSpell>().damage = damage;
        oncursor.GetComponent<OnCursorSpell>().range = range;
        oncursor.GetComponent<OnCursorSpell>().activeTime = activeTime;
        oncursor.GetComponent<OnCursorSpell>().chargeTime = chargeTime;
        oncursor.GetComponent<OnCursorSpell>().castOffset = castOffset;
        oncursor.GetComponent<OnCursorSpell>().SetTarget(mousePos);
        return true;
    }

    private bool ActivateSelfcastSpell(GameObject caster)
    {
        prefab.GetComponent<SelfcastSpell>().activeTime = activeTime;
        Instantiate(prefab, caster.transform.position, Quaternion.identity);
        return true;
    }

    private bool ActivateLaserSpell(GameObject caster, Vector3 mousePos)
    {
        GameObject laser = Instantiate(prefab, caster.transform.position, Quaternion.identity);
        laser.GetComponent<LaserSpell>().damage = damage;
        laser.GetComponent<LaserSpell>().chargeTime = chargeTime;
        laser.GetComponent<LaserSpell>().activeTime = activeTime;
        laser.GetComponent<LaserSpell>().range = range;
        laser.GetComponent<LaserSpell>().SetTarget(mousePos);
        return true;
    }
}
