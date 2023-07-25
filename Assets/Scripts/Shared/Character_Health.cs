using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Health : MonoBehaviour
{
    [SerializeField] float health = 100;
    bool isDead = false;
    float maxHealth = 0;
    private void Awake()
    {
        maxHealth = health;
    }
    public bool IsDead
    {
        get { return isDead; }
    }
    public event EventHandler HealthChanged,OnDead;
    public float Health
    {
        get { return health; }
    }
    [ContextMenu("Test die")]
    void TestDie()
    {
        TakeDamage(maxHealth);
    }
    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            health -= damage;
            if(health <= 0)
            {
                isDead = true;
                OnDead?.Invoke(this, EventArgs.Empty);
            }
            HealthChanged?.Invoke(this, EventArgs.Empty); 
        }
    }
    public float GetHealthNormalized()
    {
        return health / maxHealth;
    }
}
