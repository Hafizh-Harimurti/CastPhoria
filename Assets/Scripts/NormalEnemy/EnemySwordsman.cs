using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordsman : EntityBase
{
    
    // Start is called before the first frame update
    public float speed;
    private Transform target;
    private Animator anim;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > 0.2 )
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
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
