using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Greed Spell", menuName = "Boss Spell/Greed")]
public class SpellGreed : ScriptableObject
{
    public GameObject[] greedSpells;
    public List<GameObject> enemies;

    [NonSerialized]
    public List<GameObject> coins;
    [NonSerialized]
    public int coinStored;

    void Awake()
    {
        coins = new List<GameObject>();
    }

    public void Bribery(GameObject caster, Vector3 target)
    {
        GameObject spell = greedSpells[0];
        SpellCoin spellDetail = spell.GetComponent<SpellCoin>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = 10;
        spellDetail.enemies = enemies;
        Vector3 direction = target - caster.transform.position;
        Vector3 casterTopBound = caster.GetComponent<BoxCollider2D>().bounds.center;
        casterTopBound.y = caster.GetComponent<BoxCollider2D>().bounds.max.y;
        for (int i = 0; i < 3 + coinStored; i++)
        {
            Vector3 spellTarget = Quaternion.Euler(0,0,Random.Range(-180, 180)) * direction + caster.transform.position;
            spellDetail.target = 2 * spellTarget;
            coins.Add(Instantiate(spell, casterTopBound, Quaternion.identity));
        }
        coinStored = 0;
    }

    public void CashGrab(GameObject caster, Vector3 target)
    {
        GameObject spell = greedSpells[1];
        SpellCashGrab spellDetail = spell.GetComponent<SpellCashGrab>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.pullSpeed = 1.5f;
        spellDetail.coins = coins;
        Instantiate(spell, caster.transform.position, Quaternion.identity);
        coinStored = coins.Count;
        coins = new List<GameObject>();
    }

    public void PaidAppearance(GameObject caster, Vector3 target)
    {
        GameObject spell = greedSpells[2];
        SpellPaidAppearance spellDetail = spell.GetComponent<SpellPaidAppearance>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.coins = coins;
        spellDetail.enemies = enemies;
        Instantiate(spell, caster.transform.position, Quaternion.identity);
        coinStored = 0;
        coins = new List<GameObject>();
    }

    public void MySpellNow(GameObject caster, Vector3 target)
    {
        ElementSpell playerSpell = GameObject.FindGameObjectWithTag("Player").GetComponent<ElementSpell>();
        Element finalElement;
        if (Enumerable.SequenceEqual(playerSpell.previousElementCounts, new int[4] {0,0,0,0}))
        {
            playerSpell.spellFailcast.Failcast(new int[4] { 1, 1, 1, 1 }, caster);
            return;
        }
        for (int i = 0; i < playerSpell.previousElementCounts.Length; i++)
        {
            if (playerSpell.previousElementCounts[i] >= 2)
            {
                finalElement = (Element)(Array.FindIndex(playerSpell.previousElementCounts, e => e != 2 && e != 0) + 1);
                switch (i)
                {
                    case 0:
                        {
                            playerSpell.spellCooldownTimer[0, (int)finalElement - 1] += playerSpell.spellFire.CastSpell(finalElement, caster, target, 1)/2;
                            break;
                        }
                    case 1:
                        {
                            playerSpell.spellCooldownTimer[1, (int)finalElement - 1] += playerSpell.spellWater.CastSpell(finalElement, caster, target, 1)/2;
                            break;
                        }
                    case 2:
                        {
                            playerSpell.spellCooldownTimer[2, (int)finalElement - 1] += playerSpell.spellAir.CastSpell(finalElement, caster, target, 1)/2;
                            break;
                        }
                    case 3:
                        {
                            playerSpell.spellCooldownTimer[3, (int)finalElement - 1] += playerSpell.spellGround.CastSpell(finalElement, caster, target, 1)/2;
                            break;
                        }
                }
                return;
            }
        }
        finalElement = (Element)(Array.FindIndex(playerSpell.previousElementCounts, e => e == 0) + 1);
        playerSpell.spellCooldownTimer[4, (int)finalElement - 1] += playerSpell.spellGeneral.CastSpell(finalElement, caster, target, 1) / 2;
        return;
    }
}

public enum GreedSpell
{
    //Repeat players' last spell
    MySpellNow,
    //Throw coins
    Bribery,
    //Pull coins
    CashGrab,
    //Summon enemies on coins
    PaidAppearance,
}
