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
    public float speed;
    private Transform targetTransform;
    private Animator anim;

    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
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
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        Instantiate(fireball, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        animator.SetBool("isAttacking", false);
    }
}
