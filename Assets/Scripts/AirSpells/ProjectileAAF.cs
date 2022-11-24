using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileAAF : ProjectileBase
{
    public float damage;

    private bool isDamageDealt;
    private List<GameObject> entitiesHit;
    // Start is called before the first frame update
    void Start()
    {
        entitiesHit = new List<GameObject>();
        isDamageDealt = false;
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
        }
        isDamageDealt = true;
    }

    void GatherEntity()
    {
        Vector3 collisionCenter = new Vector3(transform.position.x + gameObject.GetComponent<BoxCollider2D>().size.x, transform.position.y, transform.position.z);
        foreach (GameObject otherGameObject in entitiesHit)
        {
            StartCoroutine(MoveEntity(otherGameObject, collisionCenter));
        }
    }

    IEnumerator MoveEntity(GameObject otherGameObject, Vector3 spellCenter)
    {
        while (!isDamageDealt)
        {
            Vector3 spellMovement = (spellCenter - otherGameObject.transform.position).normalized * moveSpeed * Time.deltaTime;
            if ((spellCenter - spellMovement).magnitude >= 0)
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
