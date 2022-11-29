using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellGGG : SpellBase
{
    public float stunDuration;

    private List<GameObject> entitiesHit;

    // Start is called before the first frame update
    void Start()
    {
        entitiesHit = new List<GameObject>();
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
        }
    }

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
