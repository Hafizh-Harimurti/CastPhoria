using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailcastBurst : SpellBase
{
    public float knockbackStrength;

    private Vector2 knockbackForce;
    private Vector2 colliderCenter;

    private void Start()
    {
        OnStart();
        colliderCenter = (Vector2)GetComponent<CircleCollider2D>().bounds.center;
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
            foreach(DebuffInfo debuff in debuffs)
            {
                entity.ApplyDebuff(debuff);
            }
            knockbackForce = ((Vector2)otherGameObject.GetComponent<BoxCollider2D>().bounds.center - colliderCenter).normalized * knockbackStrength;
            otherGameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }
}
