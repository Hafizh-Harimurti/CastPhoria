using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EntityBase
{
    public GameObject projectile;
    public GameObject target;
    private Transform targetTransform;

    public float attackTimer = 5;
    private float attackTimerCurrent;
    // Start is called before the first frame update
    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        attackTimerCurrent = 0;
        OnStart();
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
        projectile.GetComponent<ProjectileArrow>().ownerTag = gameObject.tag;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        Vector3 shootOrigin = transform.position;
        shootOrigin.y = GetComponent<BoxCollider2D>().bounds.center.y;
        Instantiate(projectile, shootOrigin, Quaternion.AngleAxis(angle, Vector3.forward));
        animator.SetBool("isAttacking", false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveEnemy(this);
    }
}
