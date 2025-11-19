using UnityEngine;

public class InterectObject : MonoBehaviour
{
    [SerializeField] Animator _animator;
    //[SerializeField] GameObject _enemy;
    //[SerializeField] private bool isEnemy;
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

                //if (isEnemy)
                //{
                //    Invoke("EnemyCreate", 1.3f);
                //    isEnemy = false;
                //}
            }

            else
            {
                _animator.SetTrigger("close");
                _isOpen = false;
            }                       
        }
    }

    

    //private void EnemyCreate()
    //{
    //    _enemy.SetActive(true);
    //}

    //private void EnemyHide()
    //{
    //    _enemy.SetActive(false);
    //}
}
