using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Enemy_Ground : MonoBehaviour
{
    Character_Attack characterAttack;
    [SerializeField] float attackCD = 2f,movementSpeed = 4f;
    [SerializeField] Transform visual,bulletSpawnPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Animator animator;
    float attackTimer = 0;
    NavMeshAgent agent;
    Character_Health health;


    private void Awake()
    {
        health = GetComponent<Character_Health>();
        agent = GetComponent<NavMeshAgent>();
        characterAttack = GetComponent<Character_Attack>();
    }
    private void Start()
    {
        characterAttack.OnStandingTooFarAway += Character_FarAway;
        characterAttack.OnAttack += Character_OnAttack;
        agent.speed = movementSpeed;
    }

    private void Character_OnAttack(object sender, EventArgs e)
    {
        agent.isStopped = true;
        visual.LookAt(new Vector3(characterAttack.CapturedTarget.position.x, visual.position.y, characterAttack.CapturedTarget.position.z));
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            LaunchProjectile();
            attackTimer = attackCD;
        }
    }
    private void Update()
    {
        if(health.IsDead)
        {
            agent.isStopped = true;
            return;
        }
        characterAttack.CheckIfTargetNullAndMakeAction();
        animator.SetFloat("Forward", agent.velocity.magnitude / agent.speed);
    }
    void LaunchProjectile()
    {
       // Debug.Log("Instantiating bullets");
        GameObject _bulletInstance = Instantiate(bullet,bulletSpawnPoint.position,Quaternion.identity);
        if(_bulletInstance.TryGetComponent<Bullet>(out Bullet _bullet))
        {
            _bullet.Setup(characterAttack.CapturedTarget.position, characterAttack.GetLayerMask(), bulletSpawnPoint.position.y,characterAttack.GetAttackDamage());
        }
    }

    private void Character_FarAway(object sender, EventArgs e)
    {
        if (health.IsDead)
        {
            agent.isStopped = true;
            return;
        }
        agent.isStopped = false;
        agent.SetDestination(characterAttack.CapturedTarget.position);
    }
   
}
