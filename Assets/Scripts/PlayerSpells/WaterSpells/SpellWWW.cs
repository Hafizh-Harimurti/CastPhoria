using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWWW : SpellBase
{
    void Start()
    {
        OnStart();
        debuffs.Add(new DebuffInfo(Debuff.Slow, effectTick, slowStrength));
        StartCoroutine(EndSpell(lifetime));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (OnTriggerEnter2DBase(collider))
        {
            StartCoroutine(ContinuousEffect(collider));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        OnTriggerExit2DBase(collider);
    }
}
