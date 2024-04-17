using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    [SerializeField]
    float damage;

    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    LayerMask whatIsEnemy;

    public void OnAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.50F, whatIsEnemy);

        foreach (var collider in colliders)
        {
            HealthController healthController = collider.GetComponent<HealthController>();
            healthController.TakeDamage(damage);
        }
    }
}
