using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public event Action<EnemyHealth> OnDeath;
    [SerializeField] private float _health = 2f;
    //[SerializeField] private ParticleSystem _particleSystem;

    public virtual void TakeDamage(float damageValue)
    {
        _health -= damageValue;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void PlayParticle(ParticleSystem _particle, Vector3 point)
    {
        _particle.transform.position = point;
        _particle.Play();
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke(this);
    }

    private void OnDestroy()
    {
        OnDeath = null;
    }
}
