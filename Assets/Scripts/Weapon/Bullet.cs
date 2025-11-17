using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PhysicsObjectsRegistry _physicsObjectsRegistry;
    private TrajectorySimulator _trajectorySimulator;
    private ParticleSystem _effect;
    private Rigidbody _rb;
    private float _damage;
    private int _maxCollisionCount;
    private int _collisionCount;

    [SerializeField] private Transform _shootPoint;

    public void InitBullet(PhysicsObjectsRegistry physicsObjectsRegistry, TrajectorySimulator trajectorySimulator, Transform shootPoint, ParticleSystem hitEffect, float damage, int maxCollisionCount)
    {
        _physicsObjectsRegistry = physicsObjectsRegistry;
        _trajectorySimulator = trajectorySimulator;
        _effect = hitEffect;
        _damage = damage;
        _maxCollisionCount = maxCollisionCount;
        _shootPoint = shootPoint;
        _collisionCount = 0;
        _rb = GetComponent<Rigidbody>();
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
            _physicsObjectsRegistry.DeleateRigitbody(_rb);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(_trajectorySimulator.IsSimulating)
        {
            _rb.linearVelocity = _rb.linearVelocity;
        }
    }
    private void PlayPartical(ParticleSystem _effect, Vector3 point)
    {
        _effect.transform.position = point;
        _effect.Play();
    }
}
