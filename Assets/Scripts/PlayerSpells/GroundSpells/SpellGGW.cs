using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGGW : SpellBase
{
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        debuffs.Add(new DebuffInfo(Debuff.Stun, stunDuration, 1));
        debuffs.Add(new DebuffInfo(Debuff.Slow, slowDuration, slowStrength));
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = direction.x < 0;
        StartCoroutine(EndSpell(lifetime));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * moveSpeed * direction;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherGameObject = collider.gameObject;
        if (!otherGameObject.CompareTag(ownerTag) && !otherGameObject.CompareTag("Projectile") && !otherGameObject.CompareTag("Spell"))
        {
            EntityBase entity = otherGameObject.GetComponent<EntityBase>();
            entity.TakeDamage(damage);
            foreach (DebuffInfo debuff in debuffs)
            {
                entity.ApplyDebuff(debuff);
            }
        }
    }
}
