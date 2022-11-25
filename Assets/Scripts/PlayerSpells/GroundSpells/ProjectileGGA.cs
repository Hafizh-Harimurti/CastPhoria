using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGGA : ProjectileBase
{
    public float damage;
    public float stunDuration;

    private List<GameObject> entitiesHit;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = (transform.position.x - owner.transform.position.x) < 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;
        if (!otherGameObject.CompareTag(owner.tag) && !otherGameObject.CompareTag("Projectile"))
        {
            entitiesHit.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        entitiesHit.Remove(collision.gameObject);
    }

    void DamageEntity()
    {
        EntityBase entity = null;
        foreach (GameObject otherGameObject in entitiesHit)
        {
            if (otherGameObject.CompareTag("Player"))
            {
                entity = otherGameObject.GetComponent<Player>();
            }
            else if (otherGameObject.CompareTag("Enemy"))
            {
                entity = otherGameObject.GetComponent<EnemyRanged>();
            }
            entity.TakeDamage(damage);
            entity.ApplyDebuff(Debuff.Stun, stunDuration, 1);
        }
    }

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
