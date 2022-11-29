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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector2.Distance(transform.position, target.position) > 0.2)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }
        
    }
    //private void OnCollisionStay2D(Collision2D other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        other.gameObject.GetComponent<PlayerHealth>().UpdateHeatlh(-attackDamage);
    //    }
    //}
    void enemyAttack(bool attacking)
    {
        anim.SetBool("isAttacking", attacking);
    }
    void enemyIdle(bool idling)
    {
        anim.SetBool("isIdle", idling);
    }
    void enemyRun(bool running)
    {
        anim.SetBool("isRunning", running);
    }
}
