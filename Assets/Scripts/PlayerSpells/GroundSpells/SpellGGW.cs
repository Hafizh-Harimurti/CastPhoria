using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGGW : SpellBase
{
    public float moveSpeed;
    public float stunDuration;
    public float slowDuration;
    public float slowStrength;
    public Vector3 direction;

    private EntityBase entity;
    private float lifetimeMax = 10;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = direction.x < 0;
        StartCoroutine(EndSpell(lifetimeMax));
    }

    // Update is called once per frame
    void Update()
    {
        if (owner == null) Destroy(gameObject);
        transform.position += Time.deltaTime * moveSpeed * direction;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherGameObject = collider.gameObject;
        if (!otherGameObject.CompareTag(owner.tag) && !otherGameObject.CompareTag("Projectile") && !otherGameObject.CompareTag("Spell"))
        {
            entity = otherGameObject.GetComponent<EntityBase>();
            entity.TakeDamage(damage);
            entity.ApplyDebuff(Debuff.Stun, stunDuration, 1);
            entity.ApplyDebuff(Debuff.Slow, slowDuration, slowStrength);
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
