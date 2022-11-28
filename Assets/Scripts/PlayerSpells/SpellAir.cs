using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Air Spell", menuName = "Spell/Air Spell")]
public class SpellAir : ScriptableObject
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
        SpellAAF spellDetail = spell.GetComponent<SpellAAF>();
        spellDetail.owner = caster;
        spellDetail.damage = 20 + (spellLevel-1) * 5;
        spellDetail.gatherSpeed = 0.2f;
        Instantiate(spell, target, Quaternion.identity);
    }

    private void AAWSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = airSpells[1];
        SpellAAW spellDetail = spell.GetComponent<SpellAAW>();
        spellDetail.owner = caster;
        spellDetail.target = target;
        spellDetail.moveSpeed = 1;
        spellDetail.lifetimeMax = 3 + (spellLevel - 1) * 0.5f;
        spellDetail.damagePerTick = 3 + (spellLevel - 1) * 1;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        Instantiate(spell, GetCasterBottomBound(caster), Quaternion.identity);
    }

    private void AAASpell(GameObject caster, Vector3 target, int spellLevel)
    {
        Vector3 castOrigin = GetCasterBottomBound(caster);
        GameObject spell = airSpells[2];
        SpellAAA spellDetail = spell.GetComponent<SpellAAA>();
        spellDetail.owner = caster;
        spellDetail.moveSpeed = 1;
        spellDetail.direction = (target - castOrigin).normalized;
        spellDetail.damagePerTick = 5 + (spellLevel - 1) * 1;
        Instantiate(spell, castOrigin, Quaternion.identity);
    }

    private void AAGSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = airSpells[3];
        SpellAAG spellDetail = spell.GetComponent<SpellAAG>();
        spellDetail.owner = caster;
        spellDetail.damage = 10 + (spellLevel - 1) * 2.5f;
        spellDetail.stunDuration = 0.5f + (spellLevel - 1) * 0.1f;
        float spellSize = spell.GetComponent<SpriteRenderer>().size.y;
        target.y += spellSize/2;
        Instantiate(spell, target, Quaternion.identity);
    }

    private Vector3 GetCasterBottomBound(GameObject caster)
    {
        Vector3 casterPos = caster.transform.position;
        casterPos.y = caster.GetComponent<BoxCollider2D>().bounds.min.y;
        return casterPos;
    }
}
