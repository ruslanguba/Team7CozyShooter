using UnityEngine;

public class SimpleDie : EnemyHealth
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    [SerializeField] private float _timeToEnableHit = 0.5f;
    private float _timer;
    [SerializeField] private bool _isCanTakeHit;
    [SerializeField] private SimpleRunEnemy _simpleRunEnemy;
    public bool IsAlive { get; private set; }

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _timer = _timeToEnableHit;
        _isCanTakeHit = false;
    }
    private void Start()
    {
        _simpleRunEnemy.GetComponent<SimpleRunEnemy>();
        IsAlive = true;
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

    public void SetImmortal(bool isImmortal)
    {
        _isCanTakeHit = isImmortal;
        _timer = _timeToEnableHit;
    }

    protected override void Die()
    {
        IsAlive = false;
        GetComponent<EnemyRotationHandler>().enabled = false;
        _simpleRunEnemy.StopMoving();

        _animator.SetTrigger("dead");
        _rigidbody.freezeRotation = false;
        _rigidbody.useGravity = true;

        base.Die();
        Destroy(gameObject, 7f);
    }
}
