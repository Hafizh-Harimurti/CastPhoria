using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;
    public float normalMoveSpeed = 1;
    public float debuffActivationTimer = 1;
    public Vector3 targetPos;
    public bool isDead;

    protected bool isGhost;
    protected bool isActive;
    protected bool isInvulnerable;
    protected bool isStunned;
    protected float moveSpeed;
    protected float damageOverTime;
    protected Animator animator;

    protected float previousAnimatorSpeed;
    protected Vector3 relativePos;
    protected List<DebuffInfo> debuffs;
    protected List<DebuffInfo> removedDebuffs;
    protected SpriteRenderer spriteRenderer;

    private bool isDebuffCoroutineOn;
    private Coroutine applyContinuousDebuff;

    protected void OnStart()
    {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        isGhost = true;
        isActive = false;
        isInvulnerable = true;
        isDead = false;
        ResetDebuff();
        health = maxHealth;
    }


    protected void OnUpdate()
    {
        DebuffEffect();
        if (!isStunned && previousAnimatorSpeed != 0)
        {
            animator.speed = previousAnimatorSpeed;
            previousAnimatorSpeed = 0;
        }
        else if (isStunned && previousAnimatorSpeed == 0)
        {
            previousAnimatorSpeed = animator.speed;
            animator.speed = 0;
        }
        CheckDeath();
    }

    protected void Move(Vector3 destination)
    {
        transform.position += destination;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isInvulnerable = true;
            isDead = true;
            isStunned = false;
            ResetDebuff();
        }
    }

    protected void ResetDebuff()
    {
        debuffs = new List<DebuffInfo>();
        removedDebuffs = new List<DebuffInfo>();
        isDebuffCoroutineOn = false;
        damageOverTime = 0;
        moveSpeed = normalMoveSpeed;
        previousAnimatorSpeed = 0;
        if (applyContinuousDebuff != null) StopCoroutine(applyContinuousDebuff);
    }

    public void ApplyDebuff(Debuff debuff, float duration, float strength)
    {
        if (isInvulnerable) return;
        int sameDebuffIndex = debuffs.FindIndex(e => (e.debuff == debuff));
        if (sameDebuffIndex == -1)
        {
            debuffs.Add(new DebuffInfo(debuff, duration, strength));
        }
        else if (debuffs[sameDebuffIndex].strength >= strength)
        {
            debuffs[sameDebuffIndex].duration = duration;
            return;
        }
        else
        {
            RemoveDebuff(debuffs[sameDebuffIndex]);
            debuffs[sameDebuffIndex] = new DebuffInfo(debuff, duration, strength);
        }
        switch (debuff)
        {
            case Debuff.Burn:
                {
                    AddDamageOverTime(strength);
                    break;
                }
            case Debuff.Slow:
                {
                    moveSpeed = normalMoveSpeed / strength;
                    break;
                }
            case Debuff.Stun:
                {
                    isActive = false;
                    isStunned = true;
                    break;
                }
        }
        if (debuffs.Count == 1 && !isDebuffCoroutineOn)
        {
            applyContinuousDebuff = StartCoroutine(ApplyContinuousDebuff());
        }
    }

    IEnumerator ApplyContinuousDebuff()
    {
        isDebuffCoroutineOn = true;
        while (debuffs.Count > 0 && !isInvulnerable)
        {
            TakeDamage(damageOverTime);
            yield return new WaitForSeconds(debuffActivationTimer);
        }
        isDebuffCoroutineOn = false;
    }

    protected void DebuffEffect()
    {
        for (int i = 0; i < debuffs.Count; i++)
        {
            debuffs[i].duration -= Time.deltaTime;
        }
        foreach(DebuffInfo debuff in debuffs)
        {
            debuff.duration -= Time.deltaTime;
            if (debuff.duration <= 0)
            {
                removedDebuffs.Add(debuff);
            }
        }
        foreach(DebuffInfo debuff in removedDebuffs)
        {
            RemoveDebuff(debuff);
        }
        if(removedDebuffs.Count > 0)
        {
            removedDebuffs = new List<DebuffInfo>();
        }
    }

    protected void RemoveDebuff(DebuffInfo debuff)
    {
        debuffs.Remove(debuff);
        switch (debuff.debuff)
        {
            case Debuff.Burn:
                {
                    RemoveDamageOverTime(debuff.strength);
                    break;
                }
            case Debuff.Slow:
                {
                    moveSpeed = normalMoveSpeed;
                    break;
                }
            case Debuff.Stun:
                {
                    isActive = true;
                    isStunned = false;
                    break;
                }
        }
    }

    protected void AddDamageOverTime(float damage)
    {
        damageOverTime += damage;
    }

    protected void RemoveDamageOverTime(float damage)
    {
        damageOverTime -= damage;
    }

    void DisableGhost()
    {
        isGhost = false;
        isInvulnerable = false;
    }

    void EnableGhost()
    {
        isGhost = true;
        isInvulnerable = true;
    }

    void DisableEntity()
    {
        isActive = false;
    }

    void EnableEntity()
    {
        isActive = true;
    }

    void SetAsSpawned()
    {
        isGhost = false;
        isInvulnerable = false;
        isActive = true;
    }

    protected void CheckDeath()
    {
        if (isDead)
        {
            isActive = false;
            isInvulnerable = true;
            animator.SetBool("isDead", true);
        }
    }

    public virtual void KillEntity()
    {
        Destroy(gameObject);
    }
}
