using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{
    public float castRange;
    public HealthBar healthBar;
    public GameManager gameState;
    public float healTimer = 2;

    [SerializeField]
    private GameObject spellTarget;

    private float moveX;
    private float moveY;
    private float spellTargetYDiff;
    private Vector3 castDirection;
    private ElementSpell elementSpell;
    private float lastHealth;
    private float healTimerCurrent;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        healTimerCurrent = 0;
        healthBar.SetMaxHealth(maxHealth);
        elementSpell = GetComponent<ElementSpell>();
        spellTargetYDiff = GetComponent<BoxCollider2D>().bounds.min.y - transform.position.y;
        targetPos = Vector3.right * castRange + transform.position;
        targetPos.y += spellTargetYDiff/2;
        spellTarget.transform.position = targetPos;
        SetAsSpawned();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
        targetPos = transform.position + castRange * castDirection;
        targetPos.y += spellTargetYDiff;
        spellTarget.transform.position = targetPos;
        if (isActive)
        {
            DoMove();
        }
        if (lastHealth == health)
        {
            healTimerCurrent += Time.deltaTime;
        }
        else
        {
            healTimerCurrent = 0;
        }
        if (healTimerCurrent >= healTimer && health <= maxHealth)
        {
            health += 0.5f;
        }
        lastHealth = health;
        PlayerInput();
        HealthUpdate();
    }

    void DoMove()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            castDirection = (Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.up * Input.GetAxisRaw("Vertical")).normalized;
            animator.SetBool("isMoving", true);
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

    void HealthUpdate()
    {
        healthBar.SetHealth(health);
    }

    void PlayerInput()
    {
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
    }
    void GameOverLose()
    {
        gameState.SceneOver(false);
    }
}
