using UnityEngine;

public class InteractableRigitbodyActivator : InteractableBase
{
    protected Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }
    public override void OnInteract()
    {
        _rb.isKinematic = false;
    }
}
