using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSloth : EntityBase
{
    public float timeBetweenSpell = 5;
    public float castRange = 2;

    [SerializeField]
    private SpellSloth spellSloth;

    private HealthBar healthBar;
    private GameObject target;
    private float cooldownTimer;
    private Vector3 vectorToTarget;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        cooldownTimer = 0;
    }

    // Update is called once per frame
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
            targetPos = vectorToTarget.magnitude > castRange ? (transform.position + vectorToTarget.normalized * castRange) : target.transform.position;
            if (cooldownTimer < timeBetweenSpell)
            {
                cooldownTimer += Time.deltaTime;
            }
            else
            {
                cooldownTimer = 0;
                if ((target.transform.position - transform.position).magnitude >= castRange/2 && Random.Range(0, 3) == 0)
                {
                    animator.SetTrigger("CastTeleport");
                }
                else
                {
                    CastSpell((SlothSpell)Random.Range(0, 3));
                }
            }
        }
        HealthUpdate();
    }

    void HealthUpdate()
    {
        healthBar.SetHealth(health);
    }

    void Teleport()
    {
        transform.position = target.transform.position;
        cooldownTimer = 2;
    }

    void CastSpell(SlothSpell slothSpell)
    {
        switch (slothSpell)
        {
            case SlothSpell.NoLife:
                {
                    animator.SetTrigger("CastNoLife");
                    break;
                }
            case SlothSpell.NoEscape:
                {
                    animator.SetTrigger("CastNoEscape");
                    break;
                }
            case SlothSpell.NoFriends:
                {
                    animator.SetTrigger("CastNoFriends");
                    break;
                }
        }
    }

    void CreateSpell(SlothSpell slothSpell)
    {
        switch(slothSpell)
        {
            case SlothSpell.NoLife:
                {
                    spellSloth.NoLife(gameObject, targetPos);
                    break;
                }
            case SlothSpell.NoEscape:
                {
                    spellSloth.NoEscape(gameObject, targetPos);
                    break;
                }
            case SlothSpell.NoFriends:
                {
                    spellSloth.NoFriends(gameObject, targetPos);
                    break;
                }
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.isBossAlive = false;
        GameManager.Instance.RemoveEnemy(this);
    }
}
