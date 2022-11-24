using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElementSpell : MonoBehaviour
{
    public ElementBar elementBar;
    public enum Element
    {
        None,
        Ground,
        Fire,
        Water,
        Air
    }

    private Element finalElement;
    private Element[] elements = new Element[3];
    private int[] elementCooldowns = new int[4];
    private int[] elementCounts = new int[4];
    private SpellAir spellAir = new SpellAir();
    
    public void AddElement(Element element)
    {
        for (int i = elements.Length - 1; i > 0; i--)
        {
            elements[i] = elements[i - 1];
        }
        elements[0] = element;
        elementBar.SetElements(elements);
    }

    public void CastElements(GameObject caster, Vector3 target)
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
                            //EarthSpell(finalElement, caster);
                            break;
                        }
                    case 1:
                        {
                            //FireSpell(elementCounts, caster);
                            break;
                        }
                    case 2:
                        {
                            //WaterSpell(elementCounts, caster);
                            break;
                        }
                    case 3:
                        {
                            spellAir.CastSpell(finalElement, caster, target);
                            break;
                        }
                }
                Array.Clear(elements, 0, elements.Length);
                elementBar.ResetElements();
                return;
            }
        }
        //GeneralSpell(elementCounts, caster);
        Array.Clear(elements, 0, elements.Length);
        elementBar.ResetElements();
        return;
    }
}
