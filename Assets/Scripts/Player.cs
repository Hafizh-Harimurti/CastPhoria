using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{
    public HealthBar healthBar;
    private ElementSpell elementSpell;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        healthBar.SetMaxHealth(maxHealth);
        elementSpell = gameObject.GetComponent<ElementSpell>();
    }

    // Update is called once per frame
    void Update()
    {
        Test();
    }

    void Test()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(15);
            healthBar.SetHealth(health);
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            elementSpell.CastSpell(gameObject);
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            elementSpell.AddElement(ElementSpell.Element.Earth);
            elementSpell.AddElement(ElementSpell.Element.Fire);
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            elementSpell.AddElement(ElementSpell.Element.Water);
            elementSpell.AddElement(ElementSpell.Element.Wind);
        }
    }
}
