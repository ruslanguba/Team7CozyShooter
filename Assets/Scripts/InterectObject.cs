using UnityEngine;

public class InterectObject : MonoBehaviour
{
    [SerializeField] Animator _animator;
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
                OpenDoor();
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
}
