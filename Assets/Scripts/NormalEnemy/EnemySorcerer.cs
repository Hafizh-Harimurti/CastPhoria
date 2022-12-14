using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySorcerer : EntityBase
{
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
        if (isActive)
        {
            relativePos = targetTransform.position - transform.position;
            spriteRenderer.flipX = relativePos.x < 0;
            if (Vector2.Distance(transform.position, targetTransform.position) > 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
            if (attackTimerCurrent >= attackTimer)
            {
                animator.SetBool("isAttacking", true);
                attackTimerCurrent = 0;
            }
        }
        attackTimerCurrent += Time.deltaTime;
        OnUpdate();
    }

    void Attack()
    {
        fireball.GetComponent<ProjectileFireball>().ownerTag = gameObject.tag;
        fireball.GetComponent<ProjectileFireball>().direction = (targetTransform.position - transform.position).normalized;
        Vector3 shootOrigin = transform.position;
        shootOrigin.y = GetComponent<BoxCollider2D>().bounds.center.y;
        Instantiate(fireball, shootOrigin, Quaternion.identity);
        animator.SetBool("isAttacking", false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveEnemy(this);
    }
}