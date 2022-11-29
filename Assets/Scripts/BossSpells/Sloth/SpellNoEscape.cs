using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellNoEscape : SpellBase
{
    public Vector3 direction;
    public float moveSpeed;
    public float stunDuration;
    public float slowDuration;
    public float slowStrength;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
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
            otherEntity.ApplyDebuff(Debuff.Stun, stunDuration, 1);
            otherEntity.ApplyDebuff(Debuff.Slow, slowDuration, slowStrength);
            animator.SetBool("isDone", true);
        }
    }

    void DestroySpell()
    {
        Destroy(gameObject);
    }
}
