using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellWWA : SpellBase
{
    public float gatherSpeed;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        debuffs.Add(new DebuffInfo(Debuff.Slow, slowDuration, slowStrength));
        StartCoroutine(EndSpell(lifetime));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnTriggerEnter2DBase(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        OnTriggerExit2DBase(collider);
    }

    public override void ImpulseEffect()
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

    void GatherEntity()
    {
        Vector3 collisionCenter = GetComponent<BoxCollider2D>().bounds.center;
        foreach (GameObject otherGameObject in entitiesHit)
        {
            StartCoroutine(MoveEntity(otherGameObject, collisionCenter));
        }
    }

    IEnumerator MoveEntity(GameObject otherGameObject, Vector3 spellCenter)
    {
        while (otherGameObject != null && entitiesHit.Contains(otherGameObject))
        {
            Vector3 spellMovement = Time.deltaTime * gatherSpeed * (spellCenter - otherGameObject.transform.position).normalized;
            if ((spellCenter - otherGameObject.transform.position - spellMovement).magnitude > 0)
            {
                otherGameObject.transform.position += spellMovement;
            }
            else if (otherGameObject.transform.position != spellCenter)
            {
                otherGameObject.transform.position = spellCenter;
            }
            yield return null;
        }
    }
}
