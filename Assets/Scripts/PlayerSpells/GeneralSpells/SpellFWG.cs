using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFWG : SpellBase
{
    public float lifetimeMax = 10;
    public float damagePerTick = 10;
    public float slowStrength = 1.5f;
    public float effectTick = 1;
    public float ministunDuration = 0.2f;

    private List<GameObject> entitiesHit;
    void Start()
    {
        entitiesHit = new List<GameObject>();
        StartCoroutine(EndSpell(lifetimeMax));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherGameObject = collider.gameObject;
        if (!otherGameObject.CompareTag(ownerTag) && !otherGameObject.CompareTag("Projectile") && !otherGameObject.CompareTag("Spell"))
        {
            entitiesHit.Add(collider.gameObject);
            StartCoroutine(DamageEntity(collider));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        entitiesHit.Remove(collider.gameObject);
    }

    IEnumerator DamageEntity(Collider2D entityCollider)
    {
        EntityBase entity = entityCollider.gameObject.GetComponent<EntityBase>();
        while (entityCollider != null && entitiesHit.Contains(entityCollider.gameObject))
        {
            entity.TakeDamage(damagePerTick);
            entity.ApplyDebuff(Debuff.Stun, ministunDuration, 1);
            entity.ApplyDebuff(Debuff.Slow, effectTick, slowStrength);
            yield return new WaitForSeconds(effectTick);
        }
    }

    IEnumerator EndSpell(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        GetComponent<Animator>().SetBool("isDone", true);
    }

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
