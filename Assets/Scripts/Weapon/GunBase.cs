using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    [SerializeField] protected Transform _spawn;
    [SerializeField] protected float _bulletSpeed = 20f;
    [SerializeField] protected float _shotPeriod = 0.5f;
    //[SerializeField] protected AudioSource _shotSound;
    //[SerializeField] protected ParticleSystem _particleFlash;

    protected float shootingTimer;
    [SerializeField] protected InputReader input;

    private void Awake()
    {
        if (input != null) 
            input = GetComponentInParent<InputReader>();
    }
    protected bool IsCanShoot()
    {
        return shootingTimer <= 0;
    }

    protected virtual void Update()
    {
        if (shootingTimer >= 0)
        {
            shootingTimer -= Time.deltaTime;
        }
    }

    public virtual void Shot()
    {
    }

    protected virtual void HideFlash() 
    {
        //_particleFlash.Play();
        //_particleFlash.SetActive(false);
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
