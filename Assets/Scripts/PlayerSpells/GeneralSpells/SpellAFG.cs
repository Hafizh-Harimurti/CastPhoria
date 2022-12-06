using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAFG : SpellBase
{
    public float knockbackStrength;

    private Vector2 knockbackForce;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        debuffs.Add(new DebuffInfo(Debuff.Stun, stunDuration, 1));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * moveSpeed * direction;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherGameObject = collider.gameObject;
        if (!otherGameObject.CompareTag(ownerTag) && (otherGameObject.CompareTag("Player") || otherGameObject.CompareTag("Enemy")))
        {
            ImpulseEffect(otherGameObject);
            DestroySpell();
        }
    }

    void ImpulseEffect(GameObject otherGameObject)
    {
        EntityBase entity = otherGameObject.GetComponent<EntityBase>();
        entity.TakeDamage(damage);
        foreach (DebuffInfo debuff in debuffs)
        {
            entity.ApplyDebuff(debuff);
        }
        knockbackForce = (otherGameObject.GetComponent<BoxCollider2D>().bounds.center - ownerPos).normalized * knockbackStrength;
        otherGameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
    }
}
