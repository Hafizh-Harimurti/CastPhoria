using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellNoEscape : SpellBase
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        animator = GetComponent<Animator>();
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
            EntityBase otherEntity = otherGameObject.GetComponent<EntityBase>();
            otherEntity.TakeDamage(damage);
            otherEntity.ApplyDebuff(new DebuffInfo(Debuff.Stun, stunDuration, 1));
            otherEntity.ApplyDebuff(new DebuffInfo(Debuff.Slow, slowDuration, slowStrength));
            animator.SetBool("isDone", true);
        }
    }
}
