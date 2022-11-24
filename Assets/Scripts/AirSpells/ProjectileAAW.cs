using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAAW : ProjectileBase
{
    public float damagePerTick = 10;
    public float slowStrength = 1.5f;
    public float effectTick = 1;
    public Vector3 target;
    private Vector3 spellMovement;
    private List<GameObject> entitiesHit;
    void Start()
    {
        entitiesHit = new List<GameObject>();
        Destroy(gameObject, lifetimeMax);
    }

    void Update()
    {
        spellMovement = (target - gameObject.transform.position).normalized * moveSpeed * Time.deltaTime;
        if ((target - spellMovement).magnitude >= 0)
        {
            gameObject.transform.position += spellMovement;
            foreach(GameObject entity in entitiesHit)
            {
                entity.transform.position += spellMovement/2;
            }
        }
        else if (gameObject.transform.position != target)
        {
            foreach (GameObject entity in entitiesHit)
            {
                entity.transform.position += (target - gameObject.transform.position)/2;
            }
            gameObject.transform.position = target;
        }
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
        } else if (entityCollision.gameObject.CompareTag("Enemy"))
        {
            entity = entityCollision.gameObject.GetComponent<EnemyRanged>();
        }
        while (entitiesHit.Contains(entityCollision.gameObject))
        {
            entity.TakeDamage(damagePerTick);
            entity.ApplyDebuff(Debuff.Slow, effectTick, slowStrength);
            yield return new WaitForSeconds(effectTick);
        }
    }
}
