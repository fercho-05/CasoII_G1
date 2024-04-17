using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    float maxHealth;

    float _currentHealth;

    public void TakeDamage(float damage)
    {
        _currentHealth -= Mathf.Abs(damage);
        _currentHealth = Mathf.Clamp(_currentHealth, 0.0F, maxHealth);

        if (_currentHealth <= 0.0F)
        {
            Destroy(gameObject);
        }
    }


}
