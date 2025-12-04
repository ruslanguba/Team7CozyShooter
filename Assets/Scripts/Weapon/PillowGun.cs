using UnityEngine;

public class PillowGun : GunBase
{

    [SerializeField] private BounceRayTrajectory _trajectory;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private ParticleSystem _hitPartical;
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletLifeTime = 8;
    [SerializeField] private int _maxCollisionCount;
    [SerializeField] private Transform _character;
    [SerializeField] private CollisionListener _collisionListener;
    [SerializeField] private bool _isActiveOnSatrt = true;
    private BulletsHandler _bulletsHandler;
    private ActorAudio _audio;
    private bool _isShowTrajectory;
    private bool _isActive;
    private bool _isAiming;   // текущее состояние
    private bool _wasAiming;  // состояние в прошлом кадре


    public override void Activate()
    {
        base.Activate();
        _isActive = true;
        _audio = GetComponentInParent<ActorAudio>();
        _bulletsHandler = GetComponent<BulletsHandler>();
        input.OnFireRealesed += StartShot;
        if(_collisionListener == null)
            _collisionListener = FindFirstObjectByType<CollisionListener>();
    }

    private void Awake()
    {
        _trajectory.GetComponent<BounceRayTrajectory>();
        _audio = GetComponentInParent<ActorAudio>();
        if(_isActiveOnSatrt)
        {
            Activate();
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _isActive = false;
        input.OnFireRealesed -= StartShot;
    }

    override protected void Update()
    {
        if (!_isActive) return;

        base.Update();

        HandleIming();
    }

    private void HandleIming()
    {
        _isAiming = input.IsAimingHeld();

        if (_isAiming && !_wasAiming)
        {
            _isShowTrajectory = true;
        }

        if (_isAiming)
        {
            CalculateTrajectory();
        }

        if (!_isAiming && _wasAiming)
        {
            _isShowTrajectory = false;
            _trajectory.Clear();
        }
        _wasAiming = _isAiming;
    }

    private void StartShot()
    {
        if (IsCanShoot())
        {
            Bullet newBullet = Instantiate(_bulletPrefab, _spawn.position, _spawn.rotation);
            LounchBullet(newBullet);
            _collisionListener.Bind(newBullet);
            //_bulletsHandler.RegisterBullet(newBullet);
            _audio.PlayAttack();
            shootingTimer = _shotPeriod;
            Destroy(newBullet.gameObject, _bulletLifeTime);

        }
    }

    private void CalculateTrajectory()
    {
        if (!_isShowTrajectory)
            return;

        Vector3 direction = _character.forward * _bulletSpeed;
        _trajectory.DrawTrajectory(direction);
    }

    private void LounchBullet(Bullet bullet)
    {
        var bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = Vector3.zero;
        bullet.InitBullet(_hitPartical, _damage, _maxCollisionCount);
        bulletRigidbody.AddForce(_character.transform.forward * _bulletSpeed, ForceMode.Impulse);
    }
}
