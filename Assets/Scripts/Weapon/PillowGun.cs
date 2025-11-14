using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowGun : Guns
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private ParticleSystem _hitPartical;
    [SerializeField] private float _damage;
    [SerializeField] private int _maxCollisionCount;
    //[SerializeField] private Transform _parent;
    private Rigidbody _bulletRigidbody;

    private void Awake()
    {
        _bullet.gameObject.SetActive(false);
        _bulletRigidbody = _bullet.GetComponent<Rigidbody>();
        _bullet.transform.parent = null;
    }

    public override void Shot()
    {
        //GameObject newBullet = Instantiate(_bulletPrefab, _spawn.position, _spawn.rotation);
        _bullet.transform.position = _spawn.position;
        _bullet.gameObject.SetActive(true);
        _bulletRigidbody.linearVelocity = Vector3.zero;
        _bullet.InitBullet(_spawn, _hitPartical, _damage, _maxCollisionCount);
        _bulletRigidbody.AddForce(_spawn.forward * _bulletSpeed, ForceMode.Impulse);
        _shotSound.Play();
        _flash.SetActive(true);
        Invoke("HideFlash", 0.1f);
    }
}
