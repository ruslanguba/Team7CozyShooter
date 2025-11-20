using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class SimpleRunEnemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _stoppingDistannce;
    private EnemyHealth _health;

    private Rigidbody _rb;
    private bool _isMoving;
    private Transform _target;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _health = GetComponent<EnemyHealth>();
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

    public void SetRunTarget(Transform target)
    {
        _target = target;
        _isMoving = true;
    }

    private void MoveTowards()
    {
        if (_isMoving)
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
        _isMoving = false;
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
