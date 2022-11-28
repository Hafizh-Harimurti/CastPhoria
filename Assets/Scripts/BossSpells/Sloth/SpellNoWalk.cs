using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellNoWalk : SpellBase
{
    void Teleport()
    {
        owner.transform.position = transform.position;
    }

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
