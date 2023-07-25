using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Character_Attack
{
   protected override void CaptureTarget()
    {
        capturedTarget = GameObject.FindWithTag("Player").transform;
    }
}
