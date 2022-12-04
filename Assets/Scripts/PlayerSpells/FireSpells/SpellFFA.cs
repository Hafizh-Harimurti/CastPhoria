using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFFA : SpellBase
{
    public float knockbackStrength;

    private Vector2 knockbackForce;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = (target.x - ownerPos.x) < 0;
    }

    private void Update()
    {
        transform.position += Time.deltaTime * moveSpeed * direction;
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
            Debug.Log(otherGameObject.name);
            entity = otherGameObject.GetComponent<EntityBase>();
            entity.TakeDamage(damage);
            knockbackForce = (otherGameObject.GetComponent<BoxCollider2D>().bounds.center - ownerPos).normalized * knockbackStrength;
            otherGameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }
}
