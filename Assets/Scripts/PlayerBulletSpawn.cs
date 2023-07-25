using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawn : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform visual,bulletSpawnPoint;
    [SerializeField] float shootCD = 1f;
    float shootTimer = 0f;
    Character_Attack characterAttack;

    private void Awake()
    {
        characterAttack = GetComponent<Character_Attack>();
    }
    private void Start()
    {
        characterAttack.OnAttack += CharacterAttack_OnAttack;
    }

    private void CharacterAttack_OnAttack(object sender, EventArgs e)
    {
        visual.LookAt(new Vector3(characterAttack.CapturedTarget.position.x, visual.position.y, characterAttack.CapturedTarget.position.z));
        if (shootTimer > 0f)
        {
            shootTimer -= Time.deltaTime;
        }
        else
        {
            GameObject _bulletInstance = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
            if (_bulletInstance.TryGetComponent<Bullet>(out Bullet _bullet))
            {
                _bullet.Setup(characterAttack.CapturedTarget.position, characterAttack.GetLayerMask(), bulletSpawnPoint.position.y, characterAttack.GetAttackDamage());
            }
            shootTimer = shootCD;   
        }
    }
}
