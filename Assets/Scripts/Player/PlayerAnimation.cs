using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private int switchTriggerId;

    private void Start()
    {
        animator = GetComponent<Animator>();

        switchTriggerId = Animator.StringToHash("Switch");
    }

    public void Switch()
    {
        animator.SetTrigger(switchTriggerId);
    }
}
