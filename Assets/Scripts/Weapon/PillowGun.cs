using UnityEngine;

public class PillowGun : GunBase
{
    [SerializeField] private BounceRayTrajectory _trajectory;
    [SerializeField] private Bullet _bulletPrefab;
    private Bullet _bullet;
    [SerializeField] private ParticleSystem _hitPartical;
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletLifeTime = 8;
    [SerializeField] private int _maxCollisionCount;
    [SerializeField] private Transform _character;
    private bool _isActive;


    public override void Activate()
    {
        base.Activate();
        _isActive = true;
        //input.OnFire += StartSimulation;
        input.OnFireRealesed += StartShot;
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _trajectory.GetComponent<BounceRayTrajectory>();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _isActive = false;
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
        }

        if (Input.GetKeyDown(KeyCode.P)) 
        {
            Vector3 direction = _character.transform.forward * _bulletSpeed;
            //_prediction.Predict(_spawn.position, direction);
        }
    }

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
            shootingTimer = _shotPeriod;
            Destroy(newBullet.gameObject, _bulletLifeTime);
        }
        _trajectory.Clear();
    }

    private void CalculateTrajectory()
    {
        Vector3 direction = _character.transform.forward * _bulletSpeed;
        _trajectory.DrawTrajectory(direction);
    }
}
