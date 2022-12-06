using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPride : EntityBase
{
    public float timeBetweenSpell = 5;
    public float castRange = 2;

    [SerializeField]
    //private SpellPride spellPride;

    private HealthBar healthBar;
    private GameObject target;
    private float cooldownTimer;
    private Vector3 vectorToTarget;

    void Start()
    {
        OnStart();
        cooldownTimer = 0;
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
                if ((target.transform.position - transform.position).magnitude >= castRange / 2 && Random.Range(0, 3) == 0)
                {
                    animator.SetTrigger("CastCharge");
                }
                else
                {
                    //CastSpell((PrideSpell)Random.Range(0, 3));
                }
            }
        }
        HealthUpdate();
    }

    void HealthUpdate()
    {
        healthBar.SetHealth(health);
    }

    

    //void CastSpell(PrideSpell prideSpell)
    //{
    //    switch (prideSpell)
    //    {
    //        case PrideSpell.NoLife:
    //            {
    //                animator.SetTrigger("CastSlash");
    //                break;
    //            }
    //        case PrideSpell.NoEscape:
    //            {
    //                animator.SetTrigger("CastCharge");
    //                break;
    //            }
    //        case PrideSpell.NoFriends:
    //            {
    //                animator.SetTrigger("CastPrideStance");
    //                break;
    //            }
    //    }
    //}

    //void CreateSpell(PrideSpell prideSpell)
    //{
    //    switch (prideSpell)
    //    {
    //        case PrideSpell.Charge:
    //            {
    //                //spellPride.Charge(gameObject, targetPos);
    //                break;
    //            }
    //        case PrideSpell.Slash:
    //            {
    //                //spellPride.Slash(gameObject, targetPos);
    //                break;
    //            }
    //        case PrideSpell.PrideStance:
    //            {
    //                //spellPride.PrideStance(gameObject, targetPos);
    //                break;
    //            }
    //    }
    //}

    private void OnDestroy()
    {
        GameManager.Instance.isBossAlive = false;
        GameManager.Instance.RemoveEnemy(this);
    }
}
