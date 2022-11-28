using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSloth : EntityBase
{
    public float timeBetweenSpell = 7;
    public float castRange = 2;

    [SerializeField]
    private SpellSloth spellSloth;

    private float cooldownTimer;
    private float spellTargetYDiff;
    private Vector3 castDirection;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        cooldownTimer = 0;
        spellTargetYDiff = GetComponent<BoxCollider2D>().bounds.min.y - transform.position.y;
        targetPos = Vector3.right * castRange + transform.position;
        targetPos.y += spellTargetYDiff;
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
        if (isActive)
        {
            castDirection = (target.transform.position - transform.position).normalized;
            targetPos = transform.position + castRange * castDirection;
            targetPos.y += spellTargetYDiff;
            if (cooldownTimer < timeBetweenSpell)
            {
                cooldownTimer += Time.deltaTime;
            }
            else
            {
                cooldownTimer = 0;
                spellSloth.CastSpell((SlothSpell)Random.Range(0, 4), gameObject, targetPos);
            }
        }
    }
}
