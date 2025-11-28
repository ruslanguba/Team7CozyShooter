using System;
using UnityEngine;

public class SimpleRunEnemy : MonoBehaviour
{
    public event Action<SimpleRunEnemy> OnDestroyed;
    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _stoppingDistannce;
    [SerializeField] private Animator _animator;
    [SerializeField] private SimpleDie _health;

    private Rigidbody _rb;
    private bool _isMoving;
    private Transform _target;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _health = GetComponent<SimpleDie>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            MoveTowards();
            CheckDistanceToTarget();
        }
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    public void SetRunTarget(Transform target)
    {
        _target = target;
        _isMoving = true;
        float randomOffset = UnityEngine.Random.Range(0f, 0.5f);
        _animator.SetBool("isMoving", _isMoving);
    }

    private void MoveTowards()
    {
        if (_isMoving && _health.IsAlive)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Vector3 targetVelocity = direction * _speed;
            Vector3 velocityDiff = targetVelocity - _rb.linearVelocity;

            _rb.AddForce(velocityDiff * _acceleration, ForceMode.Acceleration);
        }
    }

    private void CheckDistanceToTarget()
    {
        if (Vector3.Distance(transform.position, _target.position) < _stoppingDistannce)
        {
            ReachTarget();
        }
    }

    private void ReachTarget()
    {
        if (_health.IsAlive)
        {
            _health.SetImmortal(true);
            _isMoving = false;
            _animator.SetBool("isMoving", _isMoving);
            if (_target.TryGetComponent(out InteractableBox interactableBox))
            {
                interactableBox.ReciveEnemy(this);
            }
            else
            {
                _rb.linearVelocity = Vector3.zero;
                GetComponent<EnemyRotationHandler>().RoteteToPlayer(PlayerManager.Instance.GetPlayerTransform.position);
            }
        }
    }
}
