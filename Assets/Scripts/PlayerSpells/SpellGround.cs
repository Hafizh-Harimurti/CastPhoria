using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGround : MonoBehaviour
{
    public GameObject[] groundSpells;

    public void CastSpell(Element element, GameObject caster, Vector3 target, int spellLevel)
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
    }

    private void GGFSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = groundSpells[0];
        ProjectileGGF spellDetail = spell.GetComponent<ProjectileGGF>();
        spellDetail.owner = caster;
        spellDetail.stunDuration = 1 + (spellLevel - 1) * 0.15f;
        spellDetail.damage = 20 + (spellLevel - 1) * 5;
        Instantiate(spell, target, Quaternion.identity);
    }

    private void GGWSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = groundSpells[1];
        ProjectileGGW spellDetail = spell.GetComponent<ProjectileGGW>();
        spellDetail.owner = caster;
        spellDetail.stunDuration = 1 + (spellLevel - 1) * 0.15f;
        spellDetail.slowDuration = 3 + (spellLevel - 1) * 0.5f;
        spellDetail.slowStrength = 1 + (spellLevel - 1) * 0.25f;
        spellDetail.damage = 5 + (spellLevel - 1) * 1;
        spellDetail.direction = (target - caster.transform.position).normalized;
        spellDetail.moveSpeed = 2.5f;
        Instantiate(spell, caster.transform.position, Quaternion.identity);
    }

    private void GGASpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = groundSpells[2];
        ProjectileGGA spellDetail = spell.GetComponent<ProjectileGGA>();
        spellDetail.owner = caster;
        spellDetail.stunDuration = 1 + (spellLevel - 1) * 0.15f;
        spellDetail.damage = 5 + (spellLevel - 1) * 1;
        Vector3 spawnSpell = Quaternion.Euler(0, 0, -60) * (target - caster.transform.position);
        for (int i = 0; i < 3; i++)
        {
            spawnSpell = Quaternion.Euler(0, 0, 30) * spawnSpell;
            Instantiate(spell, spawnSpell + caster.transform.position, Quaternion.identity);
        }
    }

    private void GGGSpell(GameObject caster, Vector3 target, int spellLevel)
    {
        GameObject spell = groundSpells[3];
        ProjectileGGG spellDetail = spell.GetComponent<ProjectileGGG>();
        spellDetail.owner = caster;
        spellDetail.damage = 15 + (spellLevel - 1) * 5;
        spellDetail.stunDuration = 1.5f + (spellLevel - 1) * 0.2f;
        Vector3 spawnSpell = caster.transform.position;
        Vector3 direction = (target - caster.transform.position).normalized;
        for (int i = 0; i < 7; i++)
        {
            spawnSpell += direction/4;
            Instantiate(spell, spawnSpell, Quaternion.identity);
        }
    }
}
