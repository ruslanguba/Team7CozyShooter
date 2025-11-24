using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event Action<int> OnEnemyHit;
    public event Action<Bullet> OnBulletDestroyed;
    public event Action<int, Vector3> OnCollision;

    [SerializeField] private AudioClip _collisionSound;

    private ParticleSystem _effect;
    private float _damage;
    private int _maxCollisionCount;
    private int _collisionCount;

    public void InitBullet(ParticleSystem hitEffect, float damage, int maxCollisionCount)
    {
        _effect = hitEffect;
        _damage = damage;
        _maxCollisionCount = maxCollisionCount;
        _collisionCount = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisionCount++;
        Vector3 hitPoint = collision.contacts[0].point;

        // Передаём точку попадания
        OnCollision?.Invoke(_collisionCount, hitPoint);

        if (collision.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(_damage, _collisionCount);
            OnEnemyHit?.Invoke(_collisionCount);
            Debug.Log(_collisionCount);
        }

        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.OnInteract();
        }

        if (_effect != null)
        {
            PlayPartical(_effect, transform.position);
        }

        if (_collisionCount >= _maxCollisionCount)
        {
            OnBulletDestroyed?.Invoke(this);
            Destroy(gameObject, 0.01f);
        }
    }

    private void PlayPartical(ParticleSystem _effect, Vector3 point)
    {
        _effect.transform.position = point;
        _effect.Play();
    }
}
