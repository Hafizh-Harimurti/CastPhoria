using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellNoLife : SpellBase
{
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    private void OnTriggerEnter2D (Collider2D collider)
    {
        OnTriggerEnter2DBase(collider);
    }

    private void OnTriggerExit2D (Collider2D collider)
    {
        OnTriggerExit2DBase(collider);
    }
}
