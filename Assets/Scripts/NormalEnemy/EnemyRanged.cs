using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EntityBase
{
    public GameObject projectile;
    public GameObject target;
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
        projectile.GetComponent<ProjectileArrow>().ownerTag = gameObject.tag;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        Instantiate(projectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        animator.SetBool("isAttacking", false);
    }

    private void OnDestroy()
    {
        gameState.enemiesAlive.Remove(this);
        gameState.enemiesLeft--;
    }
}
