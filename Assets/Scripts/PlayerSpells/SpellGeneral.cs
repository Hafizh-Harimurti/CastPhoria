using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ground Spell", menuName = "Spell/General Spell")]
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
                    return WGASpell(caster, target, spellLevel);
                }
            case Element.Water:
                {
                    FGASpell(caster, target, spellLevel);
                    break;
                }
            case Element.Air:
                {
                    return FWGSpell(caster, target, spellLevel);
                }
            case Element.Ground:
                {
                    AFWSpell(caster, target, spellLevel);
                    break;
                }
        }
        return 0.0f;
    }

    private float WGASpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = generalSpells[0];
        SpellWGA spellDetail = spell.GetComponent<SpellWGA>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 2 + (spellLevel - 1) * 0.5f;
        spellDetail.slowDuration = 2 + (spellLevel - 1) * 0.5f;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        spellDetail.ministunDuration = 0.1f;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[0];
    }

    private void FGASpell(GameObject caster, Vector3 target, int spellLevel)
    {
        
    }

    private float FWGSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = generalSpells[2];
        SpellFWG spellDetail = spell.GetComponent<SpellFWG>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damagePerTick = 5 + (spellLevel - 1) * 1;
        spellDetail.ministunDuration = 0.1f;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[2];
    }

    private void AFWSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        
    }
}
