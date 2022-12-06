using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCoin : SpellBase
{
    public List<GameObject> enemies;

    private bool isMoving;
    private Animator animator;
    private Coroutine coroutine;
    void Start()
    {
        OnStart();
        isMoving = false;
        animator = GetComponent<Animator>();
        debuffs.Add(new DebuffInfo(Debuff.Slow, slowDuration, slowStrength));
        SetCoinMovement(target, moveSpeed, false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isMoving)
        {
            GameObject otherGameObject = collider.gameObject;
            if (otherGameObject.name == "Wall")
            {
                isMoving = false;
            }
            else if (!otherGameObject.CompareTag(ownerTag) && (otherGameObject.CompareTag("Player") || otherGameObject.CompareTag("Enemy")))
            {
                EntityBase entity = otherGameObject.GetComponent<EntityBase>();
                entity.TakeDamage(damage);
                foreach(DebuffInfo debuff in debuffs)
                {
                    entity.ApplyDebuff(debuff);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        OnTriggerExit2DBase(collider);
    }

    public void SetCoinMovement(Vector3 target, float moveSpeed, bool destroyOnArrival)
    {
        direction = (target - transform.position).normalized;
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(MoveCoin(target, direction, moveSpeed, destroyOnArrival));
    }

    IEnumerator MoveCoin(Vector3 target, Vector3 direction, float moveSpeed, bool destroyOnArrival)
    {
        isMoving = true;
        animator.SetBool("isMoving", true);
        while ((transform.position - target).sqrMagnitude > 5e-2 && isMoving)
        {
            transform.position += Time.deltaTime * moveSpeed * direction;
            yield return null;
        }
        isMoving = false;
        animator.SetBool("isMoving", false);
        if(destroyOnArrival)
        {
            Destroy(gameObject);
        }
    }

    public void StartExplosion()
    {
        animator.SetTrigger("Explode");
    }

    public override void ImpulseEffect()
    {
        base.ImpulseEffect();
        GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, Quaternion.identity);
        GameManager.Instance.AddEnemy(enemy.GetComponent<EntityBase>());
    }
}
