using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New General Spell", menuName = "Spell/General Spell")]
public class SpellGeneral : ScriptableObject
{
    public GameObject[] generalSpells;
    public float[] spellCooldowns;

    public float CastSpell(Element element, GameObject caster, Vector3 target, int spellLevel)
    {
        switch (element)
        {
            case Element.Fire:
                {
                    return AWGSpell(caster, target, spellLevel);
                }
            case Element.Water:
                {
                    return AFGSpell(caster, target, spellLevel);
                }
            case Element.Air:
                {
                    return FGWSpell(caster, target, spellLevel);
                }
            case Element.Ground:
                {
                    return FAWSpell(caster, target, spellLevel);
                }
        }
        return 0.0f;
    }

    private float AWGSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = generalSpells[0];
        SpellAWG spellDetail = spell.GetComponent<SpellAWG>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 5 + (spellLevel - 1) * 0.5f;
        spellDetail.slowDuration = 2 + (spellLevel - 1) * 0.5f;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        spellDetail.stunDuration = 0.1f;
        spellDetail.knockbackStrength = 0.2f;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[0];
    }

    private float AFGSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        Vector3 castOrigin = caster.GetComponent<BoxCollider2D>().bounds.center;
        GameObject spell = generalSpells[1];
        SpellAFG spellDetail = spell.GetComponent<SpellAFG>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.stunDuration = 0.5f + (spellLevel - 1) * 0.1f;
        spellDetail.knockbackStrength = 0.2f;
        spellDetail.damage = 5 + (spellLevel - 1) * 1;
        spellDetail.moveSpeed = 1;
        Vector3 spawnSpell = Quaternion.Euler(0, 0, -60) * (target - castOrigin);
        for (int i = 0; i < 7; i++)
        {
            spawnSpell = Quaternion.Euler(0, 0, 15) * spawnSpell;
            spellDetail.direction = spawnSpell.normalized;
            float angle = Mathf.Atan2(spawnSpell.y, spawnSpell.x) * Mathf.Rad2Deg;
            Instantiate(spell, castOrigin, Quaternion.AngleAxis(angle, Vector3.forward));
        }
        return spellCooldowns[1];
    }

    private float FGWSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = generalSpells[2];
        SpellFGW spellDetail = spell.GetComponent<SpellFGW>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damagePerTick = 5 + (spellLevel - 1) * 1;
        spellDetail.effectTick = 0.5f;
        spellDetail.stunDuration = 0.1f;
        spellDetail.slowDuration = 0.3f;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        spellDetail.lifetime = 5;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[2];
    }

    private float FAWSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = generalSpells[3];
        SpellFAW spellDetail = spell.GetComponent<SpellFAW>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 5 + (spellLevel - 1) * 0.5f;
        spellDetail.slowDuration = 2 + (spellLevel - 1) * 0.5f;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        spellDetail.gatherSpeed = 0.3f;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[3];
    }
}
