using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] Character_Attack attack;
    [SerializeField]float damage = 10f;
    private void Awake()
    {
        if(attack != null)
        {
            damage = attack.GetAttackDamage();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.TryGetComponent<Character_Health>(out var health))
            {
                health.TakeDamage(damage);
            }
        }
    }
}
