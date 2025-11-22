using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event Action<int> OnEnemyHit;
    public event Action<Bullet> OnBulletDestroyed;
    private ParticleSystem _effect;
    private Rigidbody _rb;
    private float _damage;
    private int _maxCollisionCount;
    private int _collisionCount;
    //[SerializeField] private float _speed;
    //[SerializeField] private Transform _shootPoint;

    public void InitBullet(ParticleSystem hitEffect, float damage, int maxCollisionCount)
    {
        _effect = hitEffect;
        _damage = damage;
        _maxCollisionCount = maxCollisionCount;
        _collisionCount = 0;
        _rb = GetComponent<Rigidbody>();
        //_speed = _rb.linearVelocity.magnitude;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        _collisionCount++;
        if (collision.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
            OnEnemyHit?.Invoke(_collisionCount);
            Debug.Log(_collisionCount);
            //ScoreManager.Instance.HandleHit(_collisionCount);
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
