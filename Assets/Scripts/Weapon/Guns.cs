using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private float _shotPeriod = 0.5f;
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private GameObject _flash;

    private float shootingTimer;
    private InputReader input;    

    public void SetInputReader(InputReader reader)
    {
        input = reader;
        if (input != null)
            input.OnFire += OnFireAction;
    }

    //private void OnEnable()
    //{
    //    if (InputReader.Instance != null)
    //        InputReader.Instance.OnFire += OnFireAction;
    //}

    private void OnFireAction()
    {
        if (IsCanShoot())
        {
            Shot();
            shootingTimer = _shotPeriod;
        }
    }

    private bool IsCanShoot()
    {
        return shootingTimer <= 0;
    }

    void Update()
    {
        if (shootingTimer >= 0)
        {
            shootingTimer -= Time.deltaTime;
        }
    }  

    public virtual void Shot()
    {
        GameObject newBullet = Instantiate(_bulletPrefab, _spawn.position, _spawn.rotation);
        newBullet.GetComponent<Rigidbody>().linearVelocity = _spawn.forward * _bulletSpeed;
        _shotSound.Play();
        _flash.SetActive(true);
        Invoke("HideFlash", 0.1f);
    }

    void HideFlash() 
    {
        _flash.SetActive(false);
    }

    public virtual void Activate() 
    {
        gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public virtual void AddBullets(int numberOfBullets)
    {
        
    }
}
