using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Sloth Spell", menuName ="Boss Spell/Sloth")]
public class SpellSloth : ScriptableObject
{
    public GameObject[] slothSpells;

    public void CastSpell(SlothSpell slothSpell, GameObject caster, Vector3 target)
    {
        switch (slothSpell)
        {
            case SlothSpell.NoLife:
                {
                    NoLife(caster, target);
                    break;
                }
            case SlothSpell.NoEscape:
                {
                    
                    break;
                }
            case SlothSpell.NoWalk:
                {
                    
                    break;
                }
            case SlothSpell.NoFriends:
                {
                    
                    break;
                }
        }
    }

    private void NoLife(GameObject caster, Vector3 target)
    {
        GameObject spell = slothSpells[0];
        SpellNoLife spellDetail = spell.GetComponent<SpellNoLife>();
        spellDetail.owner = caster;
        spellDetail.damage = 60;
        Instantiate(spell, target, Quaternion.identity);
    }
}

public enum SlothSpell
{
    //Huge damage
    NoLife,
    //Huge slow
    NoEscape,
    //Teleport
    NoWalk,
    //Spawn enemies
    NoFriends
}
