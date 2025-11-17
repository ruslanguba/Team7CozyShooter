using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ParticleSystem _effect;
    private Rigidbody _rb;
    private float _damage;
    private int _maxCollisionCount;
    private int _collisionCount;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _shootPoint;

    public void InitBullet(ParticleSystem hitEffect, float damage, int maxCollisionCount)
    {
        _effect = hitEffect;
        _damage = damage;
        _maxCollisionCount = maxCollisionCount;
        _collisionCount = 0;
        _rb = GetComponent<Rigidbody>();
        _speed = _rb.linearVelocity.magnitude;
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
            Destroy(gameObject);
        }
    }

    private void PlayPartical(ParticleSystem _effect, Vector3 point)
    {
        _effect.transform.position = point;
        _effect.Play();
    }
}
