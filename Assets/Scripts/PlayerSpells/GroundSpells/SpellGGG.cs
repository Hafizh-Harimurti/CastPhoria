using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellGGG : SpellBase
{
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        debuffs.Add(new DebuffInfo(Debuff.Stun, stunDuration, 1));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnTriggerEnter2DBase(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        OnTriggerExit2DBase(collider);
    }
}
