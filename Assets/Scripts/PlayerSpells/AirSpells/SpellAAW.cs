using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAAW : SpellBase
{
    void Start()
    {
        OnStart();
        direction = (target - gameObject.transform.position).normalized;
        debuffs.Add(new DebuffInfo(Debuff.Slow, effectTick, slowStrength));
        StartCoroutine(EndSpell(lifetime));
    }

    void Update()
    {
        spellMovement = Time.deltaTime * moveSpeed * direction;
        if ((target - transform.position - spellMovement).sqrMagnitude > 1e-2)
        {
            transform.position += spellMovement;
            foreach(GameObject entity in entitiesHit)
            {
                entity.transform.position += spellMovement/2;
            }
        }
        else if (transform.position != target)
        {
            foreach (GameObject entity in entitiesHit)
            {
                entity.transform.position += (target - transform.position)/2;
            }
            transform.position = target;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
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
