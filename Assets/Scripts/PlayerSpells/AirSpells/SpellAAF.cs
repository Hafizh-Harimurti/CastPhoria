using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellAAF : SpellBase
{
    public float gatherSpeed;

    private bool isDamageDealt;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        isDamageDealt = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnTriggerEnter2DBase(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        OnTriggerExit2DBase(collider);
    }

    void DamageEntity()
    {
        EntityBase entity;
        foreach (GameObject otherGameObject in entitiesHit)
        {
            entity = otherGameObject.GetComponent<EntityBase>();
            entity.TakeDamage(damage);
        }
        isDamageDealt = true;
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
        while (otherGameObject != null && !isDamageDealt && entitiesHit.Contains(otherGameObject))
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
