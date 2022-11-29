using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellAAF : SpellBase
{
    public float gatherSpeed;

    private bool isDamageDealt;
    private List<GameObject> entitiesHit;
    // Start is called before the first frame update
    void Start()
    {
        entitiesHit = new List<GameObject>();
        isDamageDealt = false;
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

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
