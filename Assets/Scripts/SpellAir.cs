using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementSpell;

public class SpellAir
{
    public GameObject[] airSpells;

    public void CastSpell(Element element, GameObject caster, Vector3 target)
    {
        switch(element)
        {
            case Element.Ground:
                {
                    AAGSpell(caster, target);
                    break;
                }
            case Element.Fire:
                {
                    AAFSpell(caster, target);
                    break;
                }
            case Element.Water:
                {
                    AAWSpell(caster, target);
                    break;
                }
            case Element.Air:
                {
                    AAASpell(caster, target);
                    break;
                }
        }
    }

    private void AAGSpell(GameObject caster, Vector3 target)
    {

    }

    private void AAFSpell(GameObject caster, Vector3 target)
    {

    }

    private void AAWSpell(GameObject caster, Vector3 target)
    {

    }

    private void AAASpell(GameObject caster, Vector3 target)
    {

    }
}
