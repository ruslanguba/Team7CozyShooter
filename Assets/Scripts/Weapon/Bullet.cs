using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ParticleSystem _effect;
    private float _damage;
    private int _maxCollisionCount;
    private int _collisionCount;
    private Transform _shootPoint;

    public void InitBullet(Transform shootPoint, ParticleSystem hitEffect, float damage, int maxCollisionCount)
    {
        _effect = hitEffect;
        _damage = damage;
        _maxCollisionCount = maxCollisionCount;
        _shootPoint = shootPoint;
        _collisionCount = 0;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        _collisionCount++;
        if (collision.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
            ScoreManager.Instance.HandleHit(_collisionCount);
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
            gameObject.SetActive(false);
            transform.position = _shootPoint.position;
        }
    }

    private void PlayPartical(ParticleSystem _effect, Vector3 point)
    {
        _effect.transform.position = point;
        _effect.Play();
    }
}
