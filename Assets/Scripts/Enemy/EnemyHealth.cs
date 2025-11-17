using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public event Action<Rigidbody> OnDeath;
    [SerializeField] private float _health = 2f;
    [SerializeField] private UnityEvent _eventOnTakeDamage;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private LayerMask _deadLayerMask;

    public void TakeDamage(float damageValue)
    {
        _health -= damageValue;
        _eventOnTakeDamage.Invoke();

        if (_health <= 0)
        {
            ScoreManager.Instance.AddNightmare(1);
            PlayParticle(_particleSystem, transform.position);            
            Die(0.1f);
        }       
    }

    private void PlayParticle(ParticleSystem _particle, Vector3 point)
    {
        _particle.transform.position = point;
        _particle.Play();
    }

    void Die(float time)
    {
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            OnDeath?.Invoke(rigidbody);
        }
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
