using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordsman : EntityBase
{
    public GameObject target;
    // Start is called before the first frame update
    public float speed;
    private Transform targetTransform;
    private Animator anim;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            targetTransform = other.transform;
        }
    }
    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim.SetBool("isIdle", true);
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
    void enemyRun(bool running)
    {
        
    }
}
