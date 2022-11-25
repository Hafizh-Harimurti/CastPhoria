using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAAA : ProjectileBase
{
    public Vector3 direction;
    public float damagePerTick = 1.5f;
    public float effectTick = 0.5f;

    private Vector3 spellMovement;
    private List<GameObject> entitiesHit;
    // Start is called before the first frame update
    void Start()
    {
        entitiesHit = new List<GameObject>();
        Destroy(gameObject, lifetimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        spellMovement = direction * moveSpeed * Time.deltaTime;
        gameObject.transform.position += spellMovement;
        foreach (GameObject entity in entitiesHit)
        {
            entity.transform.position += spellMovement * 3 / 4;
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
        }
        else if (entityCollision.gameObject.CompareTag("Enemy"))
        {
            entity = entityCollision.gameObject.GetComponent<EnemyRanged>();
        }
        while (entitiesHit.Contains(entityCollision.gameObject))
        {
            entity.TakeDamage(damagePerTick);
            yield return new WaitForSeconds(effectTick);
        }
    }
}
