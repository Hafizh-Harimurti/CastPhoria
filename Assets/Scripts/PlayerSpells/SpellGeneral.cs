using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ground Spell", menuName = "Spell/General Spell")]
public class SpellGeneral : ScriptableObject
{
    public GameObject[] generalSpells;

    public void CastSpell(Element element, GameObject caster, Vector3 target, int spellLevel)
    {
        switch (element)
        {
            case Element.Fire:
                {
                    WGASpell(caster, target, spellLevel);
                    break;
                }
            case Element.Water:
                {
                    FGASpell(caster, target, spellLevel);
                    break;
                }
            case Element.Air:
                {
                    FWGSpell(caster, target, spellLevel);
                    break;
                }
            case Element.Ground:
                {
                    AFWSpell(caster, target, spellLevel);
                    break;
                }
        }
    }

    private void WGASpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = generalSpells[0];
        SpellWGA spellDetail = spell.GetComponent<SpellWGA>();
        spellDetail.owner = caster;
        spellDetail.damage = 2 + (spellLevel - 1) * 0.5f;
        spellDetail.slowDuration = 2 + (spellLevel - 1) * 0.5f;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        spellDetail.ministunDuration = 0.1f;
        Instantiate(spell, target, Quaternion.identity);
    }

    private void FGASpell(GameObject caster, Vector3 target, int spellLevel)
    {
        
    }

    private void FWGSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = generalSpells[2];
        SpellFWG spellDetail = spell.GetComponent<SpellFWG>();
        spellDetail.owner = caster;
        spellDetail.damagePerTick = 5 + (spellLevel - 1) * 1;
        spellDetail.ministunDuration = 0.1f;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        Instantiate(spell, target, Quaternion.identity);
    }

    private void AFWSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        
    }
}
