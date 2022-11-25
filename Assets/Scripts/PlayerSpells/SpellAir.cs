using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAir : MonoBehaviour
{
    public GameObject[] airSpells;

    public void CastSpell(Element element, GameObject caster, Vector3 target, int spellLevel)
    {
        switch(element)
        {
            case Element.Fire:
                {
                    AAFSpell(caster, target, spellLevel);
                    break;
                }
            case Element.Water:
                {
                    AAWSpell(caster, target, spellLevel);
                    break;
                }
            case Element.Air:
                {
                    AAASpell(caster, target, spellLevel);
                    break;
                }
            case Element.Ground:
                {
                    AAGSpell(caster, target, spellLevel);
                    break;
                }
        }
    }

    private void AAFSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = airSpells[0];
        ProjectileAAF spellDetail = spell.GetComponent<ProjectileAAF>();
        spellDetail.owner = caster;
        spellDetail.damage = 20 + (spellLevel-1) * 5;
        Instantiate(spell, target, Quaternion.identity);
    }

    private void AAWSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = airSpells[1];
        ProjectileAAW spellDetail = spell.GetComponent<ProjectileAAW>();
        spellDetail.owner = caster;
        spellDetail.target = target;
        spellDetail.moveSpeed = 1;
        spellDetail.lifetimeMax = 3 + (spellLevel - 1) * 0.5f;
        spellDetail.damagePerTick = 3 + (spellLevel - 1) * 1;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        Instantiate(spell, caster.transform.position, Quaternion.identity);
    }

    private void AAASpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = airSpells[2];
        ProjectileAAA spellDetail = spell.GetComponent<ProjectileAAA>();
        spellDetail.owner = caster;
        spellDetail.moveSpeed = 1;
        spellDetail.direction = (target - caster.transform.position).normalized;
        spellDetail.damagePerTick = 5 + (spellLevel - 1) * 1;
        Instantiate(spell, caster.transform.position, Quaternion.identity);
    }

    private void AAGSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = airSpells[3];
        ProjectileAAG spellDetail = spell.GetComponent<ProjectileAAG>();
        spellDetail.owner = caster;
        spellDetail.damage = 10 + (spellLevel - 1) * 2.5f;
        spellDetail.stunDuration = 0.5f + (spellLevel - 1) * 0.1f;
        Instantiate(spell, target, Quaternion.identity);
    }
}
