using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySorcerer : EntityBase
{

    // Start is called before the first frame update
    public float speed;
    private Transform targetTransform;
    private float relativePosX;

    public GameObject fireball;
    public float attackTimer = 5;
    private float attackTimerCurrent;

    void Start()
    {
        OnStart();
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        attackTimerCurrent = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, targetTransform.position) > 1 && isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        relativePosX = targetTransform.position.x - transform.position.x;

        spriteRenderer.flipX = relativePosX < 0;

        attackTimerCurrent += Time.deltaTime;
        if(attackTimerCurrent >= attackTimer)
        {
            animator.SetBool("isAttacking", true);
            attackTimerCurrent = 0;
        }
        CheckDeath();
    }

    void Attack()
    {
        fireball.GetComponent<ProjectileFireball>().ownerTag = gameObject.tag;
        fireball.GetComponent<ProjectileFireball>().direction = (targetTransform.position - transform.position).normalized;
        Instantiate(fireball, transform.position, Quaternion.identity);
        animator.SetBool("isAttacking", false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveEnemy(this);
    }
}