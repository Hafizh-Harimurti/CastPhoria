using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fire Spell", menuName = "Spell/Fire Spell")]
public class SpellFire : ScriptableObject
{
    public GameObject[] fireSpells;
    public float[] spellCooldowns;

    public float CastSpell(Element element, GameObject caster, Vector3 target, int spellLevel)
    {
        switch (element)
        {
            case Element.Fire:
                {
                    return FFFSpell(caster, target, spellLevel);
                }
            case Element.Water:
                {
                    return FFWSpell(caster, target, spellLevel);
                }
            case Element.Air:
                {
                    return FFASpell(caster, target, spellLevel);
                }
            case Element.Ground:
                {
                    return FFGSpell(caster, target, spellLevel);
                }
        }
        return 0.0f;
    }

    private float FFFSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = fireSpells[0];
        SpellFFF spellDetail = spell.GetComponent<SpellFFF>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 40 + (spellLevel - 1) * 10;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[0];
    }

    private float FFWSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = fireSpells[1];
        SpellFFW spellDetail = spell.GetComponent<SpellFFW>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.slowDuration = 3 + (spellLevel - 1) * 0.5f;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        spellDetail.damage = 30 + (spellLevel - 1) * 7.5f;
        spellDetail.knockbackStrength = 0.2f;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[1];
    }

    private float FFASpell(GameObject caster, Vector3 target, int spellLevel)
    {
        Vector3 casterCenter = caster.GetComponent<BoxCollider2D>().bounds.center;
        GameObject spell = fireSpells[2];
        SpellFFA spellDetail = spell.GetComponent<SpellFFA>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.target = target;
        spellDetail.stunDuration = 1 + (spellLevel - 1) * 0.15f;
        spellDetail.knockbackStrength = 0.2f;
        spellDetail.damage = 30 + (spellLevel - 1) * 7.5f;
        spellDetail.moveSpeed = 1.5f;
        spellDetail.direction = (target - casterCenter).normalized;
        Instantiate(spell, casterCenter, Quaternion.identity);
        return spellCooldowns[2];
    }

    private float FFGSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        Vector3 castOrigin = GetCasterBottomBound(caster);
        GameObject spell = fireSpells[3];
        SpellFFG spellDetail = spell.GetComponent<SpellFFG>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 30 + (spellLevel - 1) * 7.5f;
        spellDetail.stunDuration = 0.5f + (spellLevel - 1) * 0.1f;
        Vector3 direction = (target - castOrigin).normalized;
        castOrigin += 0.1f * direction;
        for (int i = 0; i < 7; i++)
        {
            Instantiate(spell, castOrigin, Quaternion.identity);
            castOrigin += direction / 4;
        }
        return spellCooldowns[3];
    }

    private Vector3 GetCasterBottomBound(GameObject caster)
    {
        Vector3 casterPos = caster.transform.position;
        casterPos.y = caster.GetComponent<BoxCollider2D>().bounds.min.y;
        return casterPos;
    }
}
