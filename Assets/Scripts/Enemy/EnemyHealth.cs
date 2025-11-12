using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _health = 2f;
    [SerializeField] private UnityEvent _eventOnTakeDamage;

    public void TakeDamage(float damageValue)
    {
        _health -= damageValue;
        _eventOnTakeDamage.Invoke();

        Debug.Log(damageValue);

        if (_health <= 0)
        {
            ScoreManager.Instance.AddNightmare(1);
            Die(0.1f);           
        }       
    }

    void Die(float time)
    {
        Destroy(gameObject, time);
    }
}
