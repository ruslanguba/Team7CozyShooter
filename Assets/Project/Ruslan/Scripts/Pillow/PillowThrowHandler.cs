using UnityEngine;

public class PillowThrowHandler : MonoBehaviour
{
    [SerializeField] private float _throwForse;
    [SerializeField] private float _minForce = 5f;
    [SerializeField] private float _maxForce = 15f;
    [SerializeField] private float _chargeSpeed = 10f;
    [SerializeField] protected float _returnSpeed;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private Transform _pillow;
    private Collider _collider;
    private Rigidbody _rb;
    private InputReader _input;
    private float _currentForce;
    private bool _charging;
    private bool _isThrowed = false;
    private bool _isMovingBack = false;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
        _rb = _pillow.gameObject.GetComponent<Rigidbody>();
        _collider = _pillow.gameObject.GetComponent<Collider>();
        _collider.enabled = false;
        _rb.isKinematic = true;
    }
    
    private void ChargeForce()
    {
        if (_input.IsFiringHeld())
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
        Vector3 dir = (_throwPoint.forward + Vector3.up).normalized;
        _rb.AddForce(dir * _currentForce, ForceMode.Impulse);
        _pillow.parent = null;
    }

    private void Update()
    {
        ChargeForce();
        Recall();
    }

    private void Recall()
    {
        if (_isMovingBack)
        {
            _pillow.transform.position = Vector3.MoveTowards(_pillow.transform.position, _throwPoint.position, _returnSpeed * Time.deltaTime);
            _rb.isKinematic = true;
            if (Vector3.Distance(_pillow.transform.position, _throwPoint.position) < 0.5f)
            {
                _pillow.transform.position = _throwPoint.position;
                _isMovingBack = false;
                _isThrowed = false;
                _collider.enabled = false;
                _charging = false;
                _pillow.parent = _throwPoint;
            }
        }
    }
}
