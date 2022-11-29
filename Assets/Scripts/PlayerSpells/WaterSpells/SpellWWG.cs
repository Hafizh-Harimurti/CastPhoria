using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWWG : SpellBase
{
    public float stunDuration;

    private Vector2 knockbackForce;
    private List<GameObject> entitiesHit;

    // Start is called before the first frame update
    void Start()
    {
        entitiesHit = new List<GameObject>();
        StartCoroutine(EndSpell(2));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherGameObject = collider.gameObject;
        if (!otherGameObject.CompareTag(ownerTag) && !otherGameObject.CompareTag("Projectile") && !otherGameObject.CompareTag("Spell"))
        {
            entitiesHit.Add(collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        entitiesHit.Remove(collider.gameObject);
    }

    IEnumerator EndSpell(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        GetComponent<Animator>().SetBool("isDone", true);
    }

    void DamageEntity()
    {
        EntityBase entity = null;
        foreach (GameObject otherGameObject in entitiesHit)
        {
            entity = otherGameObject.GetComponent<EntityBase>();
            entity.TakeDamage(damage);
            entity.ApplyDebuff(Debuff.Stun, stunDuration, 1);
            knockbackForce = (transform.position - ownerPos).normalized * 0.2f;
        }
    }

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
