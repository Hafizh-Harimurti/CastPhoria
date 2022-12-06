using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Failcast", menuName ="Spell/Failcast Spell")]
public class SpellFailcast : ScriptableObject
{
    [System.Serializable]
    public class FailcastBase
    {
        public Element element;
        public float duration;
        public float damageOrStrength;
    }

    public List<FailcastBase> failcasts;

    [SerializeField]
    private GameObject failcastSpell;

    private List<FailcastBase> totalFailcasts;
    private List<DebuffInfo> totalDebuffs;
    private GameObject spell;

    public void Failcast(int[] elementCounts, GameObject caster)
    {
        totalFailcasts = new List<FailcastBase>();
        for(int i = 0; i < 4; i++)
        {
            totalFailcasts.Add(new FailcastBase());
            totalFailcasts[i].duration *= elementCounts[i];
        }
        spell = Instantiate(failcastSpell, caster.transform.position, Quaternion.identity);
        FailcastBurst spellDetail = spell.GetComponent<FailcastBurst>();
        spellDetail.ownerTag = caster.tag;
        spellDetail.ownerPos = caster.transform.position;
        spellDetail.damage = totalFailcasts[0].damageOrStrength * elementCounts[0];
        spellDetail.knockbackStrength = 0.1f * elementCounts[2];
        totalDebuffs = new List<DebuffInfo>();
        totalDebuffs.Add(new DebuffInfo(Debuff.Slow, totalFailcasts[1].duration * elementCounts[1], totalFailcasts[1].damageOrStrength));
        totalDebuffs.Add(new DebuffInfo(Debuff.Stun, totalFailcasts[3].duration * elementCounts[3], totalFailcasts[3].damageOrStrength));
        spellDetail.debuffs = totalDebuffs;
    }
}
