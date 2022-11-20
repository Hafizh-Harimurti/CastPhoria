using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EntityBase
{
    public GameObject arrow;
    public GameObject target;
    private EntityBase entityBase;
    private SpriteRenderer spriteRenderer;
    private Vector3 targetPos;
    private Vector3 relativePos;

    private bool isActive;
    private bool isDead;
    private float moveX;
    private float moveY;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        entityBase = GetComponent<EntityBase>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isActive = false;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            targetPos = target.transform.position;
            targetPos.z = 0;
            Move();
            DoAttack();
            if(Input.GetKeyDown(KeyCode.L))
            {
                animator.SetBool("isKilled", true);
            }
        }
    }

    private void LateUpdate()
    {
        if(isDead)
        {
            Destroy(gameObject);
        }
    }

    void ToggleEnemy()
    {
        isActive = !isActive;
    }

    void ToggleDeath()
    {
        isDead = !isDead;
    }

    void Move()
    {
        moveX = Input.GetAxis("Horizontal") * entityBase.moveSpeed * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * entityBase.moveSpeed * Time.deltaTime;
        if (moveX != 0 || moveY != 0)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        transform.position += new Vector3(moveX, moveY, 0);
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
    
    void DoAttack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isAttacking", true);
        }
    }

    void Attack()
    {
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        Instantiate(arrow, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        animator.SetBool("isAttacking", false);
    }
}
