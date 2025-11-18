using UnityEngine;

public class InterectEnemy : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _enemy;
    [SerializeField] private bool isEnemy;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            _animator.SetTrigger("open");

            if (isEnemy)
            {             
                Invoke("EnemyCreate", 1.3f);
                isEnemy = false;
            }
            

        }

        //if (collision.gameObject.TryGetComponent(out InterectEnemy enemy))
        //{
        //    _animator.SetTrigger("open close");
        //    Invoke("EnemyHide", 1f);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyMovement enemyMovement))
        {
            enemyMovement.enabled = false;
            _animator.SetTrigger("open");
            Invoke("EnemyHide", 1.3f);
            isEnemy = true;
        }
    }

    private void EnemyCreate()
    {
        _enemy.SetActive(true);
    }

    private void EnemyHide()
    {
        _enemy.SetActive(false);
    }
}
