using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailcastBurst : SpellBase
{
    public List<DebuffInfo> debuffs;
    public float knockbackStrength;

    private List<GameObject> entitiesHit;
    private Vector3 knockbackForce;
    private Vector2 colliderCenter;

    private void Start()
    {
        entitiesHit = new List<GameObject>();
        colliderCenter = (Vector2)GetComponent<CircleCollider2D>().bounds.center;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherGameObject = collider.gameObject;
        if (!otherGameObject.CompareTag(ownerTag) && !otherGameObject.CompareTag("Projectile") && !otherGameObject.CompareTag("Spell"))
        {
            entitiesHit.Add(collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        entitiesHit.Remove(collider.gameObject);
    }
    void DamageEntity()
    {
        EntityBase entity = null;
        foreach (GameObject otherGameObject in entitiesHit)
        {
            entity = otherGameObject.GetComponent<EntityBase>();
            entity.TakeDamage(damage);
            foreach(DebuffInfo debuff in debuffs)
            {
                entity.ApplyDebuff(debuff.debuff, debuff.duration, debuff.strength);
            }
            knockbackForce = ((Vector2)otherGameObject.GetComponent<BoxCollider2D>().bounds.center - colliderCenter).normalized * knockbackStrength;
            otherGameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
