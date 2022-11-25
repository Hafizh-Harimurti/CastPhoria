using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ProjectileGGW : ProjectileBase
{
    public float damage;
    public float stunDuration;
    public float slowDuration;
    public float slowStrength;
    public Vector3 direction;

    private EntityBase entity;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = direction.x < 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;
        if (!otherGameObject.CompareTag(owner.tag) && !otherGameObject.CompareTag("Projectile"))
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
            entity.ApplyDebuff(Debuff.Slow, slowDuration, slowStrength);
        }
    }
}
