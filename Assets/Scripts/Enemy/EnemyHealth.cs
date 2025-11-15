using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _health = 2f;
    [SerializeField] private UnityEvent _eventOnTakeDamage;
    [SerializeField] private ParticleSystem _particleSystem;

    public void TakeDamage(float damageValue)
    {
        _health -= damageValue;
        _eventOnTakeDamage.Invoke();

        Debug.Log(damageValue);

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
        Destroy(gameObject, time);
    }
}
