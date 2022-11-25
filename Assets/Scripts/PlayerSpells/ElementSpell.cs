using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class ElementSpell : MonoBehaviour
{
    public ElementBar elementBar;

    [SerializeField]
    private SpellAir spellAir;
    [SerializeField]
    private SpellGround spellGround;
    [SerializeField]
    private SpellGeneral spellGeneral;

    private Element finalElement;
    private Element[] elements;
    private int[] elementCooldowns;
    private int[] elementCounts;

    private void Start()
    {
        elements = new Element[3];
        elementCooldowns = new int[4];
        elementCounts = new int[4];
    }

    public void AddElement(Element element)
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
                            spellAir.CastSpell(finalElement, caster, target, spellLevel);
                            break;
                        }
                    case 3:
                        {
                            spellGround.CastSpell(finalElement, caster, target, spellLevel);
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
