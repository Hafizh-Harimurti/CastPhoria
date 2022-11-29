using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CooldownNotification : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CreateNotification(string text)
    {
        textMeshProUGUI.text = text;
        animator.SetTrigger("ShowNotification");
    }
}
