using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float moveSpeed;

    protected Animator animator;

    protected void OnStart()
    {
        health = maxHealth;
        animator = GetComponentInParent<Animator>();
    }

    protected void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            animator.SetBool("isKilled", true);
        }
    }
}
