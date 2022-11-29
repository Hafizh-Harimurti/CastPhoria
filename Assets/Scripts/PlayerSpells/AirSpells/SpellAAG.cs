using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAAG : SpellBase
{
    public float stunDuration;
    public float knockbackStrength;

    private List<GameObject> entitiesHit;
    private Vector2 colliderCenter;
    private Vector2 knockbackForce;
    // Start is called before the first frame update
    void Start()
    {
        entitiesHit = new List<GameObject>();
        colliderCenter = gameObject.GetComponent<BoxCollider2D>().bounds.center;
    }

    private void Update()
    {
        if (owner == null) Destroy(gameObject);
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
    void DamageEntity()
    {
        EntityBase entity = null;
        foreach (GameObject otherGameObject in entitiesHit)
        {
            entity = otherGameObject.GetComponent<EntityBase>();
            entity.TakeDamage(damage);
            entity.ApplyDebuff(Debuff.Stun, stunDuration, 1);
            knockbackForce = ((Vector2)otherGameObject.GetComponent<BoxCollider2D>().bounds.center - colliderCenter).normalized * knockbackStrength;
            otherGameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
