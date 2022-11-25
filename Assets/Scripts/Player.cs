using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{
    public float castRange;
    public HealthBar healthBar;

    private float moveX;
    private float moveY;
    private ElementSpell elementSpell;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        healthBar.SetMaxHealth(maxHealth);
        elementSpell = GetComponent<ElementSpell>();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
        if (isActive)
        {
            DoMove();
            DoAttack();
        }
        Test();
        HealthUpdate();
    }

    void DoMove()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            targetPos = (Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.up * Input.GetAxisRaw("Vertical")).normalized * castRange + transform.position;
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
        Move(new Vector3(moveX, moveY, 0));
    }

    void DoAttack()
    {
        
    }

    void HealthUpdate()
    {
        healthBar.SetHealth(health);
    }

    void Test()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(15);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            elementSpell.CastElements(gameObject, targetPos);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            elementSpell.AddElement(Element.Fire);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            elementSpell.AddElement(Element.Water);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            elementSpell.AddElement(Element.Air);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            elementSpell.AddElement(Element.Ground);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetBool("isDead", true);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ApplyDebuff(Debuff.Slow, 20, 2);
            ApplyDebuff(Debuff.Stun, 1, 1);
            ApplyDebuff(Debuff.Burn, 5, 2);
        }
    }
}
