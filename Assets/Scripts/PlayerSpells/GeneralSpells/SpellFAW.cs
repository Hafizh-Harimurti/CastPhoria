using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFAW : SpellBase
{
    public float gatherSpeed;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        debuffs.Add(new DebuffInfo(Debuff.Slow, slowDuration, slowStrength));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnTriggerEnter2DBase(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        OnTriggerExit2DBase(collider);
    }

    public override void ImpulseEffect()
    {
        EntityBase entity;
        foreach (GameObject otherGameObject in entitiesHit)
        {
            entity = otherGameObject.GetComponent<EntityBase>();
            entity.TakeDamage(damage);
            GatherEntity(otherGameObject, target);
        }
    }

    void GatherEntity(GameObject otherGameObject, Vector3 spellCenter)
    {
        Vector3 spellMovement = gatherSpeed * (spellCenter - otherGameObject.transform.position).normalized;
        if ((spellCenter - otherGameObject.transform.position).magnitude > spellMovement.magnitude)
        {
            otherGameObject.GetComponent<Rigidbody2D>().AddForce((Vector2)spellMovement, ForceMode2D.Impulse);
        }
        else if (otherGameObject.transform.position != spellCenter)
        {
            otherGameObject.transform.position = spellCenter;
        }
    }
}
