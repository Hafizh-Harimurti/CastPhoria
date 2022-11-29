using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWWW : SpellBase
{
    public float lifetimeMax = 3;
    public float moveSpeed = 0;
    public float damagePerTick = 5;
    public float slowStrength = 3f;
    public float effectTick = 2;
    public Vector3 target;

    private Vector3 direction;
    private Vector3 spellMovement;
    private List<GameObject> entitiesHit;
    void Start()
    {
        entitiesHit = new List<GameObject>();
        direction = (target - gameObject.transform.position).normalized;
        StartCoroutine(EndSpell(lifetimeMax));
    }

    void Update()
    {
        spellMovement = Time.deltaTime * moveSpeed *direction;
        if ((target - gameObject.transform.position - spellMovement).sqrMagnitude > 0.005)
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
