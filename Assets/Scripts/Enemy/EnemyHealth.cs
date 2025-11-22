using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public event Action<EnemyHealth, int> OnDeath;
    [SerializeField] private float _health = 2f;
    [SerializeField] private ParticleSystem _particleSystem;
    protected int _collisionsCount;

    public virtual void TakeDamage(float damageValue, int collisionsCount)
    {
        _health -= damageValue;
        _collisionsCount = collisionsCount;
        if (_health <= 0)
        {
            _particleSystem.Play();
            Die();
        }
    }

    //private void PlayParticle()
    //{
    //    _particleSystem.transform.position = point;
    //    _particle.Play();
    //}

    protected virtual void Die()
    {
        OnDeath?.Invoke(this, _collisionsCount);
    }

    private void OnDestroy()
    {
        OnDeath = null;
    }
}
