using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellNoLife : SpellBase
{
    public Vector3 target;
    public float idleDuration;

    private List<GameObject> entitiesHit;
    // Start is called before the first frame update
    void Start()
    {
        entitiesHit = new List<GameObject>();
        StartCoroutine(ExplodeSpell(idleDuration));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (true)
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
    }

    IEnumerator ExplodeSpell(float idleDuration)
    {
        yield return new WaitForSeconds(idleDuration);
        //animator.SetBool("isExploding",true);
    }
}
