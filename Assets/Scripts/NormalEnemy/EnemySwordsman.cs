using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordsman : EntityBase
{

    // Start is called before the first frame update
    public float speed;
    public GameObject projectile;
    public GameObject target;
    public GameState gameState;
    private Transform targetTransform;
    private Animator anim;
    private float relativePosX;
    public float attackTimer = 5;
    private float attackTimerCurrent;

    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        attackTimerCurrent = 0;
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");
        OnUpdate();
        attackTimerCurrent += Time.deltaTime;
        if (attackTimerCurrent >= attackTimer)
        {
            animator.SetBool("isAttacking", true);
            attackTimerCurrent = 0;
        }
        Move();
    }

    void Move()
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
        relativePos = targetTransform.position - transform.position;
        relativePosX = relativePos.x;

        spriteRenderer.flipX = relativePosX < 0;
    }

    void Attack()
    {

        projectile.GetComponent<ProjectileArrow>().ownerTag = gameObject.tag;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        Instantiate(projectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        animator.SetBool("isAttacking", false);
    }



}
