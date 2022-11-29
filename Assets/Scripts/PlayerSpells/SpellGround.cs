using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ground Spell", menuName = "Spell/Ground Spell")]
public class SpellGround : ScriptableObject
{
    public GameObject[] groundSpells;
    public float[] spellCooldowns;

    public float CastSpell(Element element, GameObject caster, Vector3 target, int spellLevel)
    {
        switch (element)
        {
            case Element.Fire:
                {
                    GGFSpell(caster, target, spellLevel);
                    break;
                }
            case Element.Water:
                {
                    GGWSpell(caster, target, spellLevel);
                    break;
                }
            case Element.Air:
                {
                    GGASpell(caster, target, spellLevel);
                    break;
                }
            case Element.Ground:
                {
                    GGGSpell(caster, target, spellLevel);
                    break;
                }
        }
        return 0.0f;
    }

    private float GGFSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = groundSpells[0];
        SpellGGF spellDetail = spell.GetComponent<SpellGGF>();
        spellDetail.owner = caster;
        spellDetail.stunDuration = 1 + (spellLevel - 1) * 0.15f;
        spellDetail.damage = 20 + (spellLevel - 1) * 5;
        Instantiate(spell, target, Quaternion.identity);
        return spellCooldowns[0];
    }

    private float GGWSpell(GameObject caster, Vector3 target, int spellLevel)
    {

        Vector3 castOrigin = GetCasterBottomBound(caster);
        GameObject spell = groundSpells[1];
        SpellGGW spellDetail = spell.GetComponent<SpellGGW>();
        spellDetail.owner = caster;
        spellDetail.stunDuration = 1 + (spellLevel - 1) * 0.15f;
        spellDetail.slowDuration = 3 + (spellLevel - 1) * 0.5f;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        spellDetail.damage = 5 + (spellLevel - 1) * 1;
        spellDetail.direction = (target - castOrigin).normalized;
        spellDetail.moveSpeed = 2.5f;
        Instantiate(spell, castOrigin, Quaternion.identity);
        return spellCooldowns[1];
    }

    private float GGASpell(GameObject caster, Vector3 target, int spellLevel)
    {
        Vector3 castOrigin = GetCasterBottomBound(caster);
        GameObject spell = groundSpells[2];
        SpellGGA spellDetail = spell.GetComponent<SpellGGA>();
        spellDetail.owner = caster;
        spellDetail.stunDuration = 1 + (spellLevel - 1) * 0.15f;
        spellDetail.damage = 5 + (spellLevel - 1) * 1;
        Vector3 spawnSpell = Quaternion.Euler(0, 0, -60) * (target - castOrigin);
        for (int i = 0; i < 3; i++)
        {
            spawnSpell = Quaternion.Euler(0, 0, 30) * spawnSpell;
            Instantiate(spell, spawnSpell + castOrigin, Quaternion.identity);
        }
        return spellCooldowns[2];
    }

    private float GGGSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        Vector3 castOrigin = GetCasterBottomBound(caster);
        GameObject spell = groundSpells[3];
        SpellGGG spellDetail = spell.GetComponent<SpellGGG>();
        spellDetail.owner = caster;
        spellDetail.damage = 15 + (spellLevel - 1) * 5;
        spellDetail.stunDuration = 1.5f + (spellLevel - 1) * 0.2f;
        Vector3 direction = (target - castOrigin).normalized;
        for (int i = 0; i < 7; i++)
        {
            castOrigin += direction/4;
            Instantiate(spell, castOrigin, Quaternion.identity);
        }
        return spellCooldowns[3];
    }

    private Vector3 GetCasterBottomBound(GameObject caster)
    {
        Vector3 casterPos = caster.transform.position;
        casterPos.y = caster.GetComponent<BoxCollider2D>().bounds.center.y;
        return casterPos;
    }
}
