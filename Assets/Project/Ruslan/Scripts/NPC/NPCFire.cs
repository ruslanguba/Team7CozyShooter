using System.Collections;
using UnityEngine;

public class NPCFire : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawn;
    [SerializeField] private Transform _target;
    [SerializeField] private float _bulletSpeed = 2;
    [SerializeField] private float _shootingRate = 3;
    [SerializeField] private float _bulletLifeTime =2;
    private GameObject _bullet;
    private Rigidbody _rb;

    private void Start()
    {
        _bullet = Instantiate(_bulletPrefab, _spawn.position, _spawn.rotation);
        _rb = _bullet.GetComponent<Rigidbody>();
        _bullet.transform.parent = null;
        _bullet.SetActive(false);

        StartCoroutine(ShootingRoutine());
    }
    IEnumerator ShootingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_shootingRate);
            _rb.linearVelocity = Vector3.zero;
            _bullet.transform.position = _spawn.transform.position;
            _bullet.SetActive(true);
            _rb.AddForce(GetRandomDirection() * _bulletSpeed, ForceMode.Impulse);
            yield return new WaitForSeconds(_bulletLifeTime);
            _bullet.SetActive(false);
        }
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 randomInside = Random.insideUnitSphere;
        Vector3 rndTarget = _target.transform.position + randomInside;
        Vector3 rndDirection = rndTarget - _spawn.transform.position;
        return rndDirection;
    }
}
