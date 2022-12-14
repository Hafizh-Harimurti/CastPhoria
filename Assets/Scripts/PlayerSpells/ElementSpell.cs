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

    [NonSerialized]
    public int[] previousElementCounts;

    public SpellFire spellFire;
    public SpellWater spellWater;
    public SpellAir spellAir;
    public SpellGround spellGround;
    public SpellGeneral spellGeneral;
    public SpellFailcast spellFailcast;

    public float[,] spellCooldownTimer;

    private Element finalElement;
    private Element[] elements;
    private float[] elementCooldowns;
    private int[] elementCounts;

    void Start()
    {
        elements = new Element[3];
        elementCooldowns = new float[4];
        elementCounts = new int[4];
        previousElementCounts = new int[4] { 0, 0, 0, 0 };
        spellCooldownTimer = new float[5, 4];
        for (int i = 0; i < 4; i++) elementCooldowns[i] = 0;
        for (int i = 0; i < 5; i++) for (int j = 0; j < 4; j++) spellCooldownTimer[i, j] = 0;
    }

    void Update()
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
        if (elementCooldowns[(int)element - 1] > 0.0f)
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

    public bool CastElements(GameObject caster, Vector3 target, int spellLevel = 1)
    {
        if (elements.All(e => e == Element.None)) return false;
        Array.Clear(elementCounts, 0, elementCounts.Length);
        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i] == Element.None)
            {
                CastFailcast(caster);
                return true;
            }
            elementCounts[(int)elements[i]-1]++;
            elements[i] = Element.None;
        }
        for (int i = 0; i < elementCounts.Length; i++)
        {
            if (elementCounts[i] >= 2)
            {
                finalElement = (Element)(Array.FindIndex(elementCounts, e => e != 2 && e != 0) + 1);
                switch (i)
                {
                    case 0:
                        {
                            if (spellCooldownTimer[0, (int)finalElement - 1] > 0)
                            {
                                CastFailcast(caster);
                                return true;
                            }
                            else
                            {
                                spellCooldownTimer[0, (int)finalElement - 1] += spellFire.CastSpell(finalElement, caster, target, spellLevel);
                            }
                            break;
                        }
                    case 1:
                        {
                            if (spellCooldownTimer[1, (int)finalElement - 1] > 0)
                            {
                                CastFailcast(caster);
                                return true;
                            }
                            else
                            {
                                spellCooldownTimer[1, (int)finalElement - 1] += spellWater.CastSpell(finalElement, caster, target, spellLevel);
                            }
                            break;
                        }
                    case 2:
                        {
                            if (spellCooldownTimer[2, (int)finalElement - 1] > 0)
                            {
                                CastFailcast(caster);
                                return true;
                            }
                            else
                            {
                                spellCooldownTimer[2, (int)finalElement - 1] += spellAir.CastSpell(finalElement, caster, target, spellLevel);
                            }
                            break;
                        }
                    case 3:
                        {
                            if (spellCooldownTimer[3, (int)finalElement - 1] > 0)
                            {
                                CastFailcast(caster);
                                return true;
                            }
                            else
                            {
                                spellCooldownTimer[3, (int)finalElement - 1] += spellGround.CastSpell(finalElement, caster, target, spellLevel);
                            }
                            break;
                        }
                }
                elementBar.ResetElements();
                previousElementCounts = elementCounts;
                return true;
            }
        }
        finalElement = (Element)(Array.FindIndex(elementCounts, e => e == 0) + 1);
        if (spellCooldownTimer[4, (int)finalElement - 1] > 0)
        {
            CastFailcast(caster);
            return true;
        }
        else
        {
            spellCooldownTimer[4, (int)finalElement - 1] += spellGeneral.CastSpell(finalElement, caster, target, spellLevel);
        }
        elementBar.ResetElements();
        previousElementCounts = elementCounts;
        return true;
    }

    void CastFailcast(GameObject caster)
    {
        spellFailcast.Failcast(elementCounts, caster);
        for (int i = 0; i < elementCounts.Length; i++)
        {
            elementCooldowns[i] += elementCounts[i] * 2;
        }
        elementBar.ResetElements();
    }
}
