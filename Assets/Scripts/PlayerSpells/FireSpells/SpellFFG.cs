using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellFFG : SpellBase
{
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        debuffs.Add(new DebuffInfo(Debuff.Stun, stunDuration, 1));
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = (transform.position.x - ownerPos.x) < 0;
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
