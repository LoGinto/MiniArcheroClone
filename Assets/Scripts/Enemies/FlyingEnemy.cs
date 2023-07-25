using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform visual,attackPoint;
    [SerializeField] float speed = 4f,noticeDistance = 6f,attackDistance = 4f,moveAmount = 4f,moveCooldown = 4f,moveTime = 3f;
    float moveTimer,moveCooldownTimer;
    bool detectedPlayer = false;
    Character_Health health;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        health = GetComponent<Character_Health>();  
        moveTimer = moveTime;
        moveCooldownTimer = moveCooldown;
        health.OnDead += Health_OnDead;
    }

    private void Health_OnDead(object sender, EventArgs e)
    {
        visual.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!detectedPlayer)
        {
            if (CloseEnough(player.position, transform.position, noticeDistance))
            {
                detectedPlayer = true;
            }
        }
        else
        {
            visual.LookAt(new Vector3(player.position.x, visual.position.y, player.position.z));
            if (moveTimer > 0)
            {
                if (Vector3.Distance(transform.position, player.position) > attackDistance)
                {
                    transform.position += (player.position - transform.position).normalized * speed * moveAmount * Time.deltaTime;
                }
                else
                {
                    moveTimer = 0;
                }
                moveCooldownTimer = moveCooldown;
                moveTimer -= Time.deltaTime; 
            }
            else
            {
                if (moveCooldownTimer > 0)
                {
                    moveCooldownTimer -= Time.deltaTime;
                }
                else
                {
                    moveTimer = moveTime;
                }
            }
        }
    }
    bool CloseEnough(Vector3 one, Vector3 two, float distance)
    {
        return (one - two).sqrMagnitude
                <= distance * distance;
    }
    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, noticeDistance);
    }
}
