using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGGF : SpellBase
{
    public float stunDuration;

    private List<GameObject> entitiesHit;

    // Start is called before the first frame update
    void Start()
    {
        entitiesHit = new List<GameObject>();
        StartCoroutine(ExplodeSpell(2));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;
        if (!otherGameObject.CompareTag(owner.tag) && !otherGameObject.CompareTag("Projectile") && !otherGameObject.CompareTag("Spell"))
        {
            entitiesHit.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        entitiesHit.Remove(collision.gameObject);
    }

    IEnumerator ExplodeSpell(float idleDuration)
    {
        yield return new WaitForSeconds(idleDuration);
        //animator.SetBool("isExploding",true);
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
            entity.ApplyDebuff(Debuff.Stun, stunDuration, 1);
        }
    }

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
