using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAAA : SpellBase
{
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        StartCoroutine(EndSpell(lifetime));
    }

    // Update is called once per frame
    void Update()
    {
        spellMovement = Time.deltaTime * moveSpeed * direction;
        transform.position += spellMovement;
        foreach (GameObject entity in entitiesHit)
        {
            entity.transform.position += spellMovement * 3 / 4;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(OnTriggerEnter2DBase(collider))
        {
            StartCoroutine(ContinuousEffect(collider));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        OnTriggerExit2DBase(collider);
    }
}
