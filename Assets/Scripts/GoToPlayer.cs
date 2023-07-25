using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToPlayer : MonoBehaviour
{
    [SerializeField]NavMeshAgent agent;
    [SerializeField] Transform player;
    [ContextMenu("Go To player")]
    void MoveToPlayer()
    {
        agent.SetDestination(player.position);
    }
}
