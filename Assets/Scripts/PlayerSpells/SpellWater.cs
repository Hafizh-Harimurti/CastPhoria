using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Water Spell", menuName = "Spell/Water Spell")]
public class SpellWater : ScriptableObject
{
    public GameObject[] waterSpells;
    public float[] spellCooldowns;

    public float CastSpell(Element element, GameObject caster, Vector3 target, int spellLevel)
    {
        switch (element)
        {
            case Element.Fire:
                {
                    return WWFSpell(caster, target, spellLevel);
                }
            case Element.Water:
                {
                    return WWWSpell(caster, target, spellLevel);
                }
            case Element.Air:
                {
                    return WWASpell(caster, target, spellLevel);
                }
            case Element.Ground:
                {
                    return WWGSpell(caster, target, spellLevel);
                }
        }
        return 0.0f;
    }

    private float WWFSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = waterSpells[0];
        SpellWWF spellDetail = spell.GetComponent<SpellWWF>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 15 + (spellLevel - 1) * 2.5f;
        spellDetail.knockbackStrength = 0.2f;
        spellDetail.slowDuration = 2 + (spellLevel - 1) * 0.5f;
        spellDetail.slowStrength = 3;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[0];
    }

    private float WWWSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = waterSpells[1];
        SpellWWW spellDetail = spell.GetComponent<SpellWWW>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.target = target;
        spellDetail.lifetime = 3 + (spellLevel - 1) * 0.5f;
        spellDetail.damagePerTick = 3 + (spellLevel - 1) * 1;
        spellDetail.effectTick = 0.5f;
        spellDetail.slowStrength = 3 + (spellLevel - 1) * 0.75f;
        spellDetail.slowDuration = 1;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[1];
    }

    private float WWASpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = waterSpells[2];
        SpellWWA spellDetail = spell.GetComponent<SpellWWA>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 3 + (spellLevel - 1) * 1;
        spellDetail.gatherSpeed = 0.1f;
        spellDetail.slowStrength = 2 + (spellLevel - 1) * 0.5f;
        spellDetail.slowDuration = 1;
        spellDetail.lifetime = 3 + (spellLevel - 1) * 0.5f;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[2];
    }

    private float WWGSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = waterSpells[3];
        SpellWWG spellDetail = spell.GetComponent<SpellWWG>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 10 + (spellLevel - 1) * 2.5f;
        spellDetail.stunDuration = 0.5f + (spellLevel - 1) * 0.1f;
        spellDetail.knockbackStrength = 0.2f;
        spellDetail.slowDuration = 2.5f + (spellLevel - 1) * 0.6f;
        spellDetail.slowStrength = 3;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[3];
    }
}
