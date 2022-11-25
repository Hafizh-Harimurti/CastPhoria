using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ElementBar : MonoBehaviour
{
    public Image[] images;
    public Sprite[] sprites;
    
    public void ResetElements()
    {
        images[0].sprite = sprites[0];
        images[1].sprite = sprites[0];
        images[2].sprite = sprites[0];
    }

    public void SetElements(Element[] elements)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            images[i].sprite = sprites[(int)elements[i]];
        }
    }
}
