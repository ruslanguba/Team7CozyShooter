using UnityEngine;

public class PillowGun : GunBase
{
    [SerializeField] private PhysicsObjectsRegistry _physicsObjectsRegistry;
    [SerializeField] private Bullet _bulletPrefab;
    private Bullet _bullet;
    [SerializeField] private ParticleSystem _hitPartical;
    [SerializeField] private float _damage;
    [SerializeField] private int _maxCollisionCount;
    [SerializeField] private TrajectorySimulator _trajectorySimulator;
    [SerializeField] private Transform _character;
    private Rigidbody _bulletRigidbody;
    private bool _isFirePressed;
    private bool _isActive;

    private void Awake()
    {
        //if(_bullet == null)
        //    _bullet = Instantiate(_bulletPrefab, _spawn.position, _spawn.rotation);
        //_bulletRigidbody = _bullet.GetComponent<Rigidbody>();
        //_bullet.transform.parent = null;
        //_bullet.gameObject.SetActive(false);
    }

    public override void Activate()
    {
        base.Activate();
        _isActive = true;
        input.OnFire += StartSimulation;
        input.OnFireRealesed += StartShot;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _isActive = false;
        input.OnFire -= StartSimulation;
        input.OnFireRealesed -= StartShot;
    }

    override protected void Update()
    {
        if (_isActive)
        {
            base.Update();
            CalculateTrajectory();
        }
    }
    private void StartSimulation()
    {
        _isFirePressed = true;
    }

    private void StartShot()
    {
        if (IsCanShoot())
        {
            Bullet newBullet = Instantiate(_bulletPrefab, _spawn.position, _spawn.rotation);
            var bulletRigidbody = newBullet.GetComponent<Rigidbody>();
            _physicsObjectsRegistry.RegisterNewRigitbody(bulletRigidbody);
            bulletRigidbody.linearVelocity = Vector3.zero;
            newBullet.InitBullet(_physicsObjectsRegistry, _trajectorySimulator ,_spawn, _hitPartical, _damage, _maxCollisionCount);
            bulletRigidbody.AddForce(_character.transform.forward * _bulletSpeed, ForceMode.Impulse);
            //_shotSound.Play();
            //_particleFlash.Play();
            //Invoke("HideFlash", 0.1f);
            _isFirePressed = false;
            shootingTimer = _shotPeriod;
            _trajectorySimulator.HideTrajectory();
        }
    }

    private void CalculateTrajectory()
    {
        if (_isFirePressed)
        {
            Vector3 direction = _character.transform.forward * _bulletSpeed;
            _trajectorySimulator.ShowTrajectory(_spawn.position, direction);
        }
    }
}
