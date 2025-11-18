using UnityEngine;

public class EnemyInFridge : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _enemy1;
    [SerializeField] GameObject _enemy2;
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
                    Invoke(nameof(EnemyCreate1), 1.3f);
                    Invoke(nameof(EnemyCreate2), 2.3f);
                    isEnemy = false;
                }
            }

            else
            {
                _animator.SetTrigger("close");
                _isOpen = false;
            }           
        }
    }

    private void EnemyCreate1()
    {
        _enemy1.SetActive(true);
    }

    private void EnemyCreate2()
    {
        _enemy2.SetActive(true);
    }
}
