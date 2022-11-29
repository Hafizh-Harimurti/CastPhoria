using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class ElementSpell : MonoBehaviour
{
    public ElementBar elementBar;
    public CooldownNotification cooldownNotification;

    [SerializeField]
    private SpellAir spellAir;
    [SerializeField]
    private SpellGround spellGround;
    [SerializeField]
    private SpellGeneral spellGeneral;

    private Element finalElement;
    private Element[] elements;
    private float[] elementCooldowns;
    private int[] elementCounts;
    private float[,] spellCooldownTimer;

    private void Start()
    {
        elements = new Element[3];
        elementCooldowns = new float[4] {0,0,0,0};
        elementCounts = new int[4];
        spellCooldownTimer = new float[5, 4];
        for (int i = 0; i < 5; i++) for (int j = 0; j < 4; j++) spellCooldownTimer[i, j] = 0;
    }

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (elementCooldowns[i] > 0)
            {
                elementCooldowns[i] -= Time.deltaTime;
                if (elementCooldowns[i] < 0)
                {
                    elementCooldowns[i] = 0;
                }
            }
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (spellCooldownTimer[i, j] > 0)
                {
                    spellCooldownTimer[i, j] -= Time.deltaTime;
                    if (spellCooldownTimer[i, j] < 0)
                    {
                        spellCooldownTimer[i, j] = 0;
                    }
                }
            }
        }
    }

    public void AddElement(Element element)
    {
        if (elementCooldowns[(int)element] > 0)
        {
            cooldownNotification.CreateNotification("Element on cooldown!");
        }
        else
        {
            if (elements.Contains(Element.None))
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    if (elements[i] == Element.None)
                    {
                        elements[i] = element;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < elements.Length - 1; i++)
                {
                    elements[i] = elements[i + 1];
                }
                elements[elements.Length - 1] = element;
            }
            elementBar.SetElements(elements);
        }
    }

    public void CastElements(GameObject caster, Vector3 target, int spellLevel = 1)
    {
        Array.Clear(elementCounts, 0, elementCounts.Length);
        if (elements.Contains(Element.None))
        {
            //Failcast(elements, caster);
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] != Element.None)
                {
                    elementCooldowns[(int)elements[i]-1] += 2;
                }
            }
            Array.Clear(elements, 0, elements.Length);
            elementBar.ResetElements();
            return;
        }
        for (int i = 0; i < elements.Length; i++)
        {
            elementCounts[(int)elements[i]-1]++;
        }
        for (int i = 0; i < elementCounts.Length; i++)
        {
            if (elementCounts[i] >= 2)
            {
                elementCounts[i] -= 2;
                finalElement = (Element)(Array.FindIndex(elementCounts, e => e == 1)+1);
                switch (i)
                {
                    case 0:
                        {
                            //FireSpell(finalElement, caster);
                            break;
                        }
                    case 1:
                        {
                            //WaterSpell(elementCounts, caster);
                            break;
                        }
                    case 2:
                        {
                            if (spellCooldownTimer[2, (int)finalElement] > 0)
                            {
                                //Failcast(elements, caster);
                                for (int j = 0; j < elements.Length; i++)
                                {
                                    elementCooldowns[(int)elements[j] - 1] += 2;
                                }
                                break;
                            }
                            spellCooldownTimer[2,(int)finalElement] += spellAir.CastSpell(finalElement, caster, target, spellLevel);
                            break;
                        }
                    case 3:
                        {
                            if (spellCooldownTimer[3, (int)finalElement] > 0)
                            {
                                //Failcast(elements, caster);
                                for (int j = 0; j < elements.Length; i++)
                                {
                                    elementCooldowns[(int)elements[j] - 1] += 2;
                                }
                                break;
                            }
                            spellCooldownTimer[3, (int)finalElement] += spellGround.CastSpell(finalElement, caster, target, spellLevel);
                            break;
                        }
                }
                Array.Clear(elements, 0, elements.Length);
                elementBar.ResetElements();
                return;
            }
        }
        finalElement = (Element)(Array.FindIndex(elementCounts, e => e == 0) + 1);
        spellGeneral.CastSpell(finalElement, caster, target, spellLevel);
        Array.Clear(elements, 0, elements.Length);
        elementBar.ResetElements();
        return;
    }
}
