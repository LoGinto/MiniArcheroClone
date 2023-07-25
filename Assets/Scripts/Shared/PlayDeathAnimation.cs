using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDeathAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    Character_Health health;

    void Awake()
    {
        health = GetComponent<Character_Health>();
    }
    private void Start()
    {
        health.OnDead += Health_OnDead;
    }

    private void Health_OnDead(object sender, EventArgs e)
    {
        animator.Play("Death");
    }
}
