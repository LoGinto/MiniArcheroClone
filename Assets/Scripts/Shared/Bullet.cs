using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float moveDistance = 10f,moveSpeed = 3f;
    float damage;
    float turnOffTimer, turnOffCD = 3f;
    Vector3 goToPos,moveDir;
    bool fly = false;
    LayerMask detectionLayer;

    public void Setup(Vector3 pos, LayerMask layer, float yCoordinate, float _damage)
    {
        this.goToPos = pos;
        goToPos.y = yCoordinate;
        detectionLayer = layer;
        damage = _damage;
        turnOffTimer = turnOffCD;
        moveDir = (goToPos - transform.position).normalized * moveDistance;
        fly = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fly)
        {
            if (turnOffTimer > 0)
            {
                transform.position += moveDir * moveSpeed * Time.deltaTime;
                turnOffTimer -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((detectionLayer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            fly = false;
            if (other.TryGetComponent<Character_Health>(out Character_Health health))
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
