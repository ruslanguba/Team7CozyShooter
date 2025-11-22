using UnityEngine;

public class SimpleDie : EnemyHealth
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    [SerializeField] private float _timeToEnableHit = 1;
    private float _timer;
    private bool _isCanTakeHit;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _timer = _timeToEnableHit;
        _isCanTakeHit = false;
    }

    public override void TakeDamage(float damageValue, int collisions)
    {
        if (_isCanTakeHit)
        {
            base.TakeDamage(damageValue, collisions);
        }
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _isCanTakeHit = true;
            }
        }
    }

    protected override void Die()
    {
        GetComponent<EnemyRotationHandler>().enabled = false;
        GetComponent<SimpleRunEnemy>().StopMoving();
        GetComponent<SimpleRunEnemy>().enabled = false;

        _rigidbody.freezeRotation = false;
        _rigidbody.useGravity = true;

        //_animator.SetTrigger("dead");
        base.Die();
        Destroy(gameObject, 7f);
    }
}
