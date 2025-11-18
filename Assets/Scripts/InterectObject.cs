using UnityEngine;

public class InterectObject : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _enemy;
    [SerializeField] private bool isEnemy;
    [SerializeField] private bool _isOpen;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            if (!_isOpen)
            {
                _animator.SetTrigger("open");
                _isOpen = true;

                if (isEnemy)
                {
                    Invoke("EnemyCreate", 1.3f);
                    isEnemy = false;
                }
            }

            else
            {
                _animator.SetTrigger("close");
                _isOpen = false;
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
