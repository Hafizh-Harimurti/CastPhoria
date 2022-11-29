using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Sloth Spell", menuName ="Boss Spell/Sloth")]
public class SpellSloth : ScriptableObject
{
    public GameObject[] slothSpells;

    public void NoLife(GameObject caster, Vector3 target)
    {
        GameObject spell = slothSpells[0];
        SpellNoLife spellDetail = spell.GetComponent<SpellNoLife>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 60;
        Instantiate(spell, target, Quaternion.identity);
    }

    public void NoEscape(GameObject caster, Vector3 target)
    {
        Vector3 casterCenter = caster.GetComponent<BoxCollider2D>().bounds.center;
        GameObject spell = slothSpells[1];
        SpellNoEscape spellDetail = spell.GetComponent<SpellNoEscape>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.direction = target - caster.GetComponent<BoxCollider2D>().bounds.center;
        spellDetail.moveSpeed = 1.5f;
        spellDetail.damage = 20;
        spellDetail.slowDuration = 5;
        spellDetail.stunDuration = 1;
        spellDetail.slowStrength = 4;
        Instantiate(spell, casterCenter, Quaternion.identity);
    }

    public void NoFriends(GameObject caster, Vector3 target)
    {
        GameObject spell = slothSpells[2];
        Instantiate(spell, target, Quaternion.identity);
    }
}

public enum SlothSpell
{
    //Huge damage
    NoLife,
    //Huge slow
    NoEscape,
    //Spawn enemies
    NoFriends
}
