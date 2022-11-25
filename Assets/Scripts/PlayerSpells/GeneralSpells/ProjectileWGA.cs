using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWGA : ProjectileBase
{
    public float damage;
    public float slowDuration;
    public float slowStrength;
    public float ministunDuration;

    private List<GameObject> entitiesHit;
    private Vector2 knockbackForce;
    private Vector3 castOrigin;

    // Start is called before the first frame update
    void Start()
    {
        entitiesHit = new List<GameObject>();
        castOrigin = owner.transform.position;
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
            entity.ApplyDebuff(Debuff.Slow, slowDuration, slowStrength);
            entity.ApplyDebuff(Debuff.Stun, ministunDuration, 1);
            knockbackForce = (transform.position - castOrigin).normalized * 0.15f;
            otherGameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
