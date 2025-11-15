using UnityEngine;
using UnityEngine.Windows;

public class PillowThrowHandler : GunBase
{
    [SerializeField] private float _throwForse;
    [SerializeField] private float _minForce = 5f;
    [SerializeField] private float _maxForce = 15f;
    [SerializeField] private float _chargeSpeed = 10f;
    [SerializeField] protected float _returnSpeed;
    //[SerializeField] private Transform _throwPoint;
    [SerializeField] private Transform _pillow;
    private Collider _collider;
    private Rigidbody _rb;
    //[SerializeField] private InputReader _input;
    private float _currentForce;
    [SerializeField] private bool _charging;
    [SerializeField] private bool _isThrowed = false;
    [SerializeField] private bool _isMovingBack = false;

    [SerializeField] private bool _isActive;

    public override void Activate()
    {
        base.Activate();
        _isActive = true;
        input.OnInteract += StopPillow;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _isActive = false;
        input.OnInteract -= StopPillow;
    }

    private void Awake()
    {
        _rb = _pillow.gameObject.GetComponent<Rigidbody>();
        _collider = _pillow.gameObject.GetComponent<Collider>();
        _collider.enabled = false;
        _rb.isKinematic = true;
    }
    
    private void ChargeForce()
    {
        if (input.IsFiringHeld())
        {
            _charging = true;
            _currentForce += _chargeSpeed * Time.deltaTime;
            _currentForce = Mathf.Clamp(_currentForce, _minForce, _maxForce);
            if (_isThrowed)
            {
                _isMovingBack = true;
                _charging = false;
                return;
            }
        }

        else if (_charging)
        {
            Throw();
            _charging = false;
            _currentForce = 0f;
        }
    }

    private void Throw()
    {
        _collider.enabled = true;
        _isThrowed = true;
        _rb.isKinematic = false;
        Vector3 dir = _spawn.forward.normalized;
        _rb.AddForce(dir * _currentForce, ForceMode.Impulse);
        _pillow.parent = null;
    }

    protected override void Update()
    {
        if (_isActive)
        {
            ChargeForce();
            Recall();
        }
    }

    private void Recall()
    {
        if (_isMovingBack)
        {
            _pillow.transform.position = Vector3.MoveTowards(_pillow.transform.position, _spawn.position, _returnSpeed * Time.deltaTime);
            _rb.isKinematic = true;
            if (Vector3.Distance(_pillow.transform.position, _spawn.position) < 0.5f)
            {
                _pillow.transform.position = _spawn.position;
                _isMovingBack = false;
                _isThrowed = false;
                _collider.enabled = false;
                _charging = false;
                _pillow.parent = _spawn;
            }
        }
    }

    private void StopPillow()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.isKinematic = true;
    }
}
