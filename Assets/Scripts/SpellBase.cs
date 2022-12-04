using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class SpellBase : MonoBehaviour
{
    public string ownerTag;
    public Vector3 ownerPos;

    //Spell effects
    public float slowStrength;
    public float slowDuration;
    public float stunDuration;

    //For spell with target location
    public Vector3 target;

    //For spell with direction
    public Vector3 direction;

    //For spell with movement
    public float moveSpeed;

    //For impulse-based spell
    public float damage;

    //For continuous effect spell
    public float damagePerTick;
    public float effectTick;

    //For spell with a lifespan
    public float lifetime;
    public List<DebuffInfo> debuffs;

    protected List<GameObject> entitiesHit;

    protected Vector3 spellMovement;

    protected void OnStart()
    {
        debuffs = new List<DebuffInfo>();
        entitiesHit = new List<GameObject>();
    }

    protected bool OnTriggerEnter2DBase(Collider2D collider)
    {
        GameObject otherGameObject = collider.gameObject;
        if (!otherGameObject.CompareTag(ownerTag) && !otherGameObject.CompareTag("Projectile") && !otherGameObject.CompareTag("Spell"))
        {
            entitiesHit.Add(collider.gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void OnTriggerExit2DBase(Collider2D collider)
    {
        entitiesHit.Remove(collider.gameObject);
    }

    public virtual void ImpulseEffect()
    {
        EntityBase entity;
        foreach (GameObject otherGameObject in entitiesHit)
        {
            entity = otherGameObject.GetComponent<EntityBase>();
            entity.TakeDamage(damage);
            foreach (DebuffInfo debuff in debuffs)
            {
                entity.ApplyDebuff(debuff);
            }
        }
    }

    public virtual IEnumerator ContinuousEffect(Collider2D entityCollider)
    {
        EntityBase entity = entityCollider.gameObject.GetComponent<EntityBase>();
        while (entityCollider != null && entitiesHit.Contains(entityCollider.gameObject))
        {
            entity.TakeDamage(damagePerTick);
            foreach(DebuffInfo debuff in debuffs)
            {
                entity.ApplyDebuff(debuff);
            }
            yield return new WaitForSeconds(effectTick);
        }
    }

    public virtual IEnumerator EndSpell(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        GetComponent<Animator>().SetBool("isDone", true);
    }

    protected void DestroySpell()
    {
        Destroy(gameObject);
    }
}
