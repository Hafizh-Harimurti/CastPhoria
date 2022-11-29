using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySorcerer : EntityBase
{
    public GameObject fireball;
    private GameObject target;
    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");
        OnUpdate();
        targetPos = target.transform.position;
        Move();
    }

    void Move()
    {
        relativePos = targetPos - transform.position;
        if (relativePos.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (relativePos.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void Attack()
    {
        fireball.GetComponent<ProjectileFireball>().ownerTag = gameObject.tag;
        fireball.GetComponent<ProjectileFireball>().direction = (targetPos - transform.position).normalized;
        Instantiate(fireball, transform.position, Quaternion.identity);
        animator.SetBool("isAttacking", false);
    }

    private void OnDestroy()
    {
        gameState.enemiesAlive.Remove(this);
        gameState.enemiesLeft--;
    }
}
