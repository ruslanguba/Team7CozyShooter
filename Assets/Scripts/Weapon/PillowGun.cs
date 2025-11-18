using UnityEngine;

public class PillowGun : GunBase
{
    [SerializeField] private PredictionBootstrap _prediction;
    //[SerializeField] private PhysicsObjectsRegistry _physicsObjectsRegistry;
    [SerializeField] private Bullet _bulletPrefab;
    private Bullet _bullet;
    [SerializeField] private ParticleSystem _hitPartical;
    [SerializeField] private float _damage;
    [SerializeField] private int _maxCollisionCount;
    //[SerializeField] private TrajectorySimulator _trajectorySimulator;
    [SerializeField] private Transform _character;
    private Rigidbody _bulletRigidbody;
    private bool _isFirePressed;
    private bool _isActive;

    public override void Activate()
    {
        base.Activate();
        _isActive = true;
        //input.OnFire += StartSimulation;
        input.OnFireRealesed += StartShot;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _isActive = false;
        //input.OnFire -= StartSimulation;
        input.OnFireRealesed -= StartShot;
    }

    override protected void Update()
    {
        if (_isActive)
        {
            base.Update();
            if(input.IsFiringHeld() && IsCanShoot())
            {
                CalculateTrajectory();
            }
            //CalculateTrajectory();
        }

        if (Input.GetKeyDown(KeyCode.P)) 
        {
            Vector3 direction = _character.transform.forward * _bulletSpeed;
            _prediction.Predict(_spawn.position, direction);
        }
    }
    //private void StartSimulation()
    //{
    //    if (_isFirePressed && !IsCanShoot())
    //        _isFirePressed = true;
    //}

    private void StartShot()
    {
        if (IsCanShoot())
        {
            Bullet newBullet = Instantiate(_bulletPrefab, _spawn.position, _spawn.rotation);
            var bulletRigidbody = newBullet.GetComponent<Rigidbody>();
            bulletRigidbody.linearVelocity = Vector3.zero;
            newBullet.InitBullet(_hitPartical, _damage, _maxCollisionCount);
            bulletRigidbody.AddForce(_character.transform.forward * _bulletSpeed, ForceMode.Impulse);
            //_shotSound.Play();
            //_particleFlash.Play();
            _isFirePressed = false;
            shootingTimer = _shotPeriod;
        }
        _prediction.Clear();
        //_trajectorySimulator.HideTrajectory();
    }

    private void CalculateTrajectory()
    {
        Vector3 direction = _character.transform.forward * _bulletSpeed;
        _prediction.Predict(_spawn.position, direction);
        //if (_isFirePressed && IsCanShoot())
        //{
        //    Vector3 direction = _character.transform.forward * _bulletSpeed;
        //    _prediction.Predict(_spawn.position, direction);
        //    //_trajectorySimulator.ShowTrajectory(_spawn.position, direction);
        //}
    }
}
