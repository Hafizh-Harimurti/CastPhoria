using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGreed : EntityBase
{
    public float timeBetweenSpell = 5;
    public float castRange = 2;

    [SerializeField]
    private SpellGreed spellGreed;

    private Transform targetTransform;
    private HealthBar healthBar;
    private GameObject target;
    private float cooldownTimer;
    private Vector3 vectorToTarget;

    void Start()
    {
        OnStart();
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cooldownTimer = 0;
        spellGreed.coins = new List<GameObject>();
        spellGreed.coinStored = 0;
    }

    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if (healthBar == null)
        {
            healthBar = GameObject.FindGameObjectWithTag("BossBar").GetComponent<HealthBar>();
            healthBar.SetMaxHealth(maxHealth);
        }
        OnUpdate();
        if (isActive)
        {
            vectorToTarget = target.transform.position - transform.position;
            spriteRenderer.flipX = vectorToTarget.x < 0;
            targetPos = vectorToTarget.magnitude > castRange ? (transform.position + vectorToTarget.normalized * castRange) : target.transform.position;
            if (cooldownTimer < timeBetweenSpell)
            {
                cooldownTimer += Time.deltaTime;
            }
            else
            {
                cooldownTimer = 0;
                if(spellGreed.coins.Count == 0)
                {
                    if(Random.Range(0,4) == 0)
                    {
                        CastSpell(GreedSpell.MySpellNow);
                    }
                    else
                    {
                        CastSpell(GreedSpell.Bribery);
                    }
                }
                else
                {
                    CastSpell((GreedSpell)Random.Range(0,4));
                }
            }
        }
        if (Vector2.Distance(transform.position, targetTransform.position) > 1 && isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        HealthUpdate();
    }

    void HealthUpdate()
    {
        healthBar.SetHealth(health);
    }

    void CastSpell(GreedSpell greedSpell)
    {
        switch (greedSpell)
        {
            case GreedSpell.MySpellNow:
                {
                    CreateSpell(GreedSpell.MySpellNow);
                    break;
                }
            case GreedSpell.Bribery:
                {
                    animator.SetTrigger("CastBribery");
                    break;
                }
            case GreedSpell.CashGrab:
                {
                    animator.SetTrigger("CastCashGrab");
                    break;
                }
            case GreedSpell.PaidAppearance:
                {
                    animator.SetTrigger("CastPaidAppearance");
                    break;
                }
        }
    }

    void CreateSpell(GreedSpell greedSpell)
    {
        switch (greedSpell)
        {
            case GreedSpell.MySpellNow:
                {
                    spellGreed.MySpellNow(gameObject, targetPos);
                    break;
                }
            case GreedSpell.Bribery:
                {
                    spellGreed.Bribery(gameObject, targetPos);
                    break;
                }
            case GreedSpell.CashGrab:
                {
                    spellGreed.CashGrab(gameObject, targetPos);
                    break;
                }
            case GreedSpell.PaidAppearance:
                {
                    spellGreed.PaidAppearance(gameObject, targetPos);
                    break;
                }
        }
    }

    private void OnDestroy()
    {
        foreach(GameObject coin in spellGreed.coins)
        {
            Destroy(coin);
        }
        GameManager.Instance.isBossAlive = false;
        GameManager.Instance.RemoveEnemy(this);
    }
}
