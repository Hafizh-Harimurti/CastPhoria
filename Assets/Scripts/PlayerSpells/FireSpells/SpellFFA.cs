using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFFA : SpellBase
{
    public float stunDuration;

    private List<GameObject> entitiesHit;
    private Vector2 knockbackForce;
    private Vector3 castOrigin;

    // Start is called before the first frame update
    void Start()
    {
        entitiesHit = new List<GameObject>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = (transform.position.x - owner.transform.position.x) < 0;
        castOrigin = owner.transform.position;
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
            Debug.Log(otherGameObject.name);
            entity = otherGameObject.GetComponent<EntityBase>();
            entity.TakeDamage(damage);
            knockbackForce = (transform.position - castOrigin).normalized * 0.1f;
            otherGameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
