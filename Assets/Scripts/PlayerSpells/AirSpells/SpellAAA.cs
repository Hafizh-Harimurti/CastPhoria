using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAAA : SpellBase
{
    public float moveSpeed;
    public float lifetimeMax;
    public Vector3 direction;
    public float damagePerTick = 1.5f;
    public float effectTick = 0.5f;

    private Vector3 spellMovement;
    private List<GameObject> entitiesHit;
    // Start is called before the first frame update
    void Start()
    {
        entitiesHit = new List<GameObject>();
        StartCoroutine(EndSpell(lifetimeMax));
    }

    // Update is called once per frame
    void Update()
    {
        spellMovement = Time.deltaTime *moveSpeed * direction;
        gameObject.transform.position += spellMovement;
        foreach (GameObject entity in entitiesHit)
        {
            entity.transform.position += spellMovement * 3 / 4;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherGameObject = collider.gameObject;
        if (!otherGameObject.CompareTag(owner.tag) && !otherGameObject.CompareTag("Projectile") && !otherGameObject.CompareTag("Spell"))
        {
            entitiesHit.Add(collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        entitiesHit.Remove(collider.gameObject);
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

    IEnumerator EndSpell(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        GetComponent<Animator>().SetBool("isDone", true);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
