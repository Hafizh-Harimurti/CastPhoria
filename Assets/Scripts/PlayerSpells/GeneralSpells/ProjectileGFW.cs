using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGFW : ProjectileBase
{
    public float damagePerTick = 10;
    public float slowStrength = 1.5f;
    public float effectTick = 1;
    public float ministunDuration = 0.1f;

    private List<GameObject> entitiesHit;
    void Start()
    {
        entitiesHit = new List<GameObject>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;
        if (!otherGameObject.CompareTag(owner.tag) && !otherGameObject.CompareTag("Projectile"))
        {
            entitiesHit.Add(otherGameObject);
            StartCoroutine(DamageEntity(collision));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        entitiesHit.Remove(collision.gameObject);
    }

    IEnumerator DamageEntity(Collision2D entityCollision)
    {
        EntityBase entity = null;
        if (entityCollision.gameObject.CompareTag("Player"))
        {
            entity = entityCollision.gameObject.GetComponent<Player>();
        }
        else if (entityCollision.gameObject.CompareTag("Enemy"))
        {
            entity = entityCollision.gameObject.GetComponent<EnemyRanged>();
        }
        while (entitiesHit.Contains(entityCollision.gameObject))
        {
            entity.TakeDamage(damagePerTick);
            entity.ApplyDebuff(Debuff.Stun, ministunDuration, 1);
            entity.ApplyDebuff(Debuff.Slow, effectTick, slowStrength);
            yield return new WaitForSeconds(effectTick);
        }
    }
}
