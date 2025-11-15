using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowGun : GunBase
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private ParticleSystem _hitPartical;
    [SerializeField] private float _damage;
    [SerializeField] private int _maxCollisionCount;
    [SerializeField] private TrajectorySimulator _trajectorySimulator;
    private Rigidbody _bulletRigidbody;
    private bool _isFirePressed;

    private void Awake()
    {
        _bulletRigidbody = _bullet.GetComponent<Rigidbody>();
        _bullet.transform.parent = null;
        _bullet.gameObject.SetActive(false);
    }

    public override void Activate(InputReader reader)
    {
        base.Activate(reader);
        input.OnFire += StartSimulation;
        input.OnFireRealesed += StartShot;
    }

    public override void Deactivate()
    {
        input.OnFire -= StartSimulation;
        input.OnFireRealesed -= StartShot;
    }

    override protected void Update()
    {
        base.Update();
        CalculateTrajectory();
    }
    private void StartSimulation()
    {
        _isFirePressed = true;
    }

    private void StartShot()
    {
        //GameObject newBullet = Instantiate(_bulletPrefab, _spawn.position, _spawn.rotation);
        _bullet.transform.position = _spawn.position;
        _bullet.gameObject.SetActive(true);
        _bulletRigidbody.linearVelocity = Vector3.zero;
        _bullet.InitBullet(_spawn, _hitPartical, _damage, _maxCollisionCount);
        _bulletRigidbody.AddForce(_spawn.forward * _bulletSpeed, ForceMode.VelocityChange);
        _shotSound.Play();
        _particleFlash.Play();
        Invoke("HideFlash", 0.1f);
        _isFirePressed = false;
        _trajectorySimulator.HideTrajectory();
        
    }

    private void CalculateTrajectory()
    {
        if (_isFirePressed)
        {
            Vector3 direction = _spawn.forward * _bulletSpeed;
            _trajectorySimulator.ShowTrajectory(_spawn.position, direction);
        }
    }
}
