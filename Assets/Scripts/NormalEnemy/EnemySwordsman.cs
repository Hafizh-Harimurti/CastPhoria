using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordsman : EntityBase
{

    public float speed;
    public GameObject projectile;
    public GameObject target;
    private Transform targetTransform;
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
            if (Vector2.Distance(transform.position, targetTransform.position) > 0.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
                if (attackTimerCurrent >= attackTimer)
                {
                    animator.SetBool("isAttacking", true);
                    attackTimerCurrent = 0;
                }
            }
            relativePos = targetTransform.position - transform.position;
            spriteRenderer.flipX = relativePos.x < 0;
        }
        attackTimerCurrent += Time.deltaTime;
        OnUpdate();
    }

    void Attack()
    {
        projectile.GetComponent<ProjectileSlash>().ownerTag = gameObject.tag;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        Instantiate(projectile, GetComponent<BoxCollider2D>().bounds.center, Quaternion.AngleAxis(angle, Vector3.forward));
        animator.SetBool("isAttacking", false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveEnemy(this);
    }
}
