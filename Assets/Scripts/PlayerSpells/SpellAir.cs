using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Air Spell", menuName = "Spell/Air Spell")]
public class SpellAir : ScriptableObject
{
    public GameObject[] airSpells;
    public float[] spellCooldowns;

    public float CastSpell(Element element, GameObject caster, Vector3 target, int spellLevel)
    {
        switch(element)
        {
            case Element.Fire:
                {
                    return AAFSpell(caster, target, spellLevel);
                }
            case Element.Water:
                {
                    return AAWSpell(caster, target, spellLevel);
                }
            case Element.Air:
                {
                    return AAASpell(caster, target, spellLevel);
                }
            case Element.Ground:
                {
                    return AAGSpell(caster, target, spellLevel);
                }
        }
        return 0.0f;
    }

    private float AAFSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = airSpells[0];
        SpellAAF spellDetail = spell.GetComponent<SpellAAF>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 20 + (spellLevel-1) * 5;
        spellDetail.gatherSpeed = 0.2f;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[0];
    }

    private float AAWSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = airSpells[1];
        SpellAAW spellDetail = spell.GetComponent<SpellAAW>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.target = target;
        spellDetail.moveSpeed = 1;
        spellDetail.lifetimeMax = 3 + (spellLevel - 1) * 0.5f;
        spellDetail.damagePerTick = 3 + (spellLevel - 1) * 1;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        Instantiate(spell, GetCasterBottomBound(caster), Quaternion.identity);
        return spellCooldowns[1];
    }

    private float AAASpell(GameObject caster, Vector3 target, int spellLevel)
    {
        Vector3 castOrigin = GetCasterBottomBound(caster);
        GameObject spell = airSpells[2];
        SpellAAA spellDetail = spell.GetComponent<SpellAAA>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.moveSpeed = 1;
        spellDetail.direction = (target - castOrigin).normalized;
        spellDetail.damagePerTick = 5 + (spellLevel - 1) * 1;
        Instantiate(spell, castOrigin, Quaternion.identity);
        return spellCooldowns[2];
    }

    private float AAGSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = airSpells[3];
        SpellAAG spellDetail = spell.GetComponent<SpellAAG>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 10 + (spellLevel - 1) * 2.5f;
        spellDetail.stunDuration = 0.5f + (spellLevel - 1) * 0.1f;
        float spellSize = spell.GetComponent<SpriteRenderer>().size.y;
        target.y += spellSize/2;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[3];
    }

    private Vector3 GetCasterBottomBound(GameObject caster)
    {
        Vector3 casterPos = caster.transform.position;
        casterPos.y = caster.GetComponent<BoxCollider2D>().bounds.min.y;
        return casterPos;
    }
}
