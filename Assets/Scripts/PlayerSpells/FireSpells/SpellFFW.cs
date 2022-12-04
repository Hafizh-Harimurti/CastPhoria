using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFFW : SpellBase
{
    public float knockbackStrength;

    private Vector2 knockbackForce;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        debuffs.Add(new DebuffInfo(Debuff.Slow, slowDuration, slowStrength));
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

    public override void ImpulseEffect()
    {
        foreach (GameObject otherGameObject in entitiesHit)
        {
            EntityBase entity = otherGameObject.GetComponent<EntityBase>();
            entity.TakeDamage(damage);
            foreach (DebuffInfo debuff in debuffs)
            {
                entity.ApplyDebuff(debuff);
            }
            knockbackForce = (otherGameObject.GetComponent<BoxCollider2D>().bounds.center - ownerPos).normalized * knockbackStrength;
            otherGameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }
}
