using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] private float _initialDamage = 1f;
    ////[SerializeField] private float _decayFactor = 1f;
    //[SerializeField] private int _maxHits = 5;
    //[SerializeField] private float _lifeTime = 2f;
    //[SerializeField] private float _speed = 10f;

    //private int hits = 0;
    ////private float currentDamage;
    //private bool canHitAgain = true;

    //private void Start()
    //{
    //    //currentDamage = _initialDamage;
    //    Destroy(gameObject, _lifeTime);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy") && hits < _maxHits && canHitAgain)
    //    {
    //        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
    //        if (enemyHealth != null)
    //        {
    //            enemyHealth.TakeDamage(_initialDamage);
    //            hits++;
    //            //currentDamage *= _decayFactor;
    //            canHitAgain = false;

    //            Invoke(nameof(ResetHitState), 0.2f);
    //            ScoreManager.Instance.HandleHit(hits);
    //        }
    //    }
    //}

    //private void ResetHitState()
    //{
    //    canHitAgain = true;
    //}





    [SerializeField] private GameObject _effect;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_effect != null)
        {
            GameObject systemParticle = Instantiate(_effect, transform.position, Quaternion.identity);
            Destroy(systemParticle, 2);
        }

        Destroy(gameObject);
    }
}
