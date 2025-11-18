using UnityEngine;

public class InteractableBox : InteractableBase
{
    [SerializeField] Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void OnInteract()
    {
        _animator.SetTrigger("open");
    }
}
