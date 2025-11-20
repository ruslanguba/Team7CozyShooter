using UnityEngine;

public class InterectObject : MonoBehaviour, IInteractablePlace
{
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _enemy;
    [SerializeField] private bool _isEnemyVisible;
    [SerializeField] private bool _isOpen;
    [SerializeField] private InterectObject _partnerCabinet;

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
                OpenDoor();

                if (_isEnemyVisible)
                {
                    _partnerCabinet.ReleaseEnemy();
                    CloseDoor();
                }
            }

            else
            {
                CloseDoor();
            }                       
        }
    }

    public void OpenDoor()
    {
        _animator.SetTrigger("open");
        _isOpen = true;
    }

    public void CloseDoor()
    {
        _animator.SetTrigger("close");
        _isOpen = false;
    }

    public void SetEnemyVisibility(bool visible)
    {
        _enemy.SetActive(visible);
        _isEnemyVisible = visible;
    }

    public void ReleaseEnemy()
    {
        SetEnemyVisibility(true);
        _enemy.GetComponent<RunningEnemy>().TargetCabinet = this;
    }

    public void AcceptEnemy()
    {
        SetEnemyVisibility(false);
        CloseDoor();
    }
}
