using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Attack : MonoBehaviour
{
    [SerializeField] float attackRadius = 5f,damage = 10f;
    [SerializeField] LayerMask enemyLayer;
    Collider[] results = new Collider[4];
    protected Transform capturedTarget;
    
    public float GetAttackDamage()
    {
        return damage; 
    }
    public LayerMask GetLayerMask()
    {
        return enemyLayer;
    }
    public Transform CapturedTarget { 
            get { return capturedTarget; }
    }
    public event EventHandler OnAttack,OnStandingTooFarAway;

    private void Start()
    {
        if (TryGetComponent<PlayerLocomotion>(out PlayerLocomotion characterLocomotion))
        {
            characterLocomotion.OnStandStill += Locomotion_OnStandStill;
            characterLocomotion.OnMove += Locomotion_OnMove;
        }
    }

    private void Locomotion_OnMove(object sender, EventArgs e)
    {
        if(capturedTarget != null)
        {
            capturedTarget = null;
        }
    }

    private void Locomotion_OnStandStill(object sender, EventArgs e)
    {
        CheckIfTargetNullAndMakeAction();
    }
    public void CheckIfTargetNullAndMakeAction()
    {
        if (capturedTarget == null)
        {
            CaptureTarget();
        }
        else
        {
            AttackBehaviour();
        }
    }
    void AttackBehaviour()
    {
        if (CloseEnough(transform.position, capturedTarget.position,attackRadius))
        {
            OnAttack?.Invoke(this, EventArgs.Empty);
            Debug.Log($"{gameObject.name} attacks");
        }
        else
        {
            OnStandingTooFarAway?.Invoke(this, EventArgs.Empty);
        }
    }
    bool CloseEnough(Vector3 one, Vector3 two, float distance)
    {
        return (one - two).sqrMagnitude
                <= distance * distance;
    }
    [ContextMenu("Capture Target test")]
    protected virtual void CaptureTarget()
    {
        int size = Physics.OverlapSphereNonAlloc(transform.position, attackRadius, results, enemyLayer);
        if (size > 0)
        {
            int index = 0;
            capturedTarget = results[0].transform;
            try
            {
                while (capturedTarget.GetComponent<Character_Health>().IsDead)
                {
                    index++;
                    capturedTarget = results[index].transform;
                }
            }
            catch
            {
                return;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
