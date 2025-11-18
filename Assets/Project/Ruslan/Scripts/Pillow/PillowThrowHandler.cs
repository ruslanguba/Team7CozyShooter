using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PillowThrowHandler : GunBase
{
    [Header("References")]
    [SerializeField] private Transform _spawnPoint; // точка броска
    [SerializeField] private Transform _character;
    [SerializeField] private List<GameObject> _pillows; // заранее созданные подушки
    //[SerializeField] private DynamicObjectsRegistry _dynamicObjectsRegistry;
    private Stack<GameObject> _thrownPillows = new Stack<GameObject>();

    [Header("Settings")]
    [SerializeField] private float _throwForce = 10f;
    [SerializeField] private float _recallSpeed = 12f;

    [SerializeField] private InputReader _input;

    private int _currentIndex = -1; // индекс текущей летящей подушки
    private Rigidbody _currentRb;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
        //_dynamicObjectsRegistry = GetComponentInChildren<DynamicObjectsRegistry>();
        // делаем все подушки неактивными и кинематикой на старте
        foreach (var pillow in _pillows)
        {
            pillow.SetActive(false);
            var rb = pillow.GetComponent<Rigidbody>();
            pillow.transform.parent = null;
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    private void OnEnable()
    {
        _input.OnSecondaryFire += HandleThrowOrStop;
        _input.OnRecall += HandleRecall;
    }

    private void OnDisable()
    {
        _input.OnSecondaryFire -= HandleThrowOrStop;
        _input.OnRecall -= HandleRecall;
    }

    private void HandleThrowOrStop()
    {
        // если сейчас летит подушка, останавливаем её
        if (_currentRb != null)
        {
            StopCurrentPillow();
            return;
        }

        // иначе ищем следующую доступную подушку
        GameObject nextPillow = GetNextPillow();
        if (nextPillow != null)
        {
            ThrowPillow(nextPillow);
        }
    }

    private GameObject GetNextPillow()
    {
        for (int i = 0; i < _pillows.Count; i++)
        {
            int index = (i + _currentIndex + 1) % _pillows.Count;
            if (!_pillows[index].activeInHierarchy)
            {
                _currentIndex = index;
                return _pillows[index];
            }
        }
        return null; // все подушки в полёте
    }

    private void ThrowPillow(GameObject pillow)
    {
        pillow.transform.position = _spawnPoint.position;
        pillow.transform.rotation = _character.rotation;
        pillow.SetActive(true);
        _currentRb = pillow.GetComponent<Rigidbody>();
        _currentRb.isKinematic = false;
        _currentRb.linearVelocity = Vector3.zero;
        _currentRb.angularVelocity = Vector3.zero;
        _currentRb.AddForce(_character.forward * _throwForce, ForceMode.Impulse);
        _currentRb.detectCollisions = true;
        _thrownPillows.Push(pillow); // кладём подушку в стек
        //_dynamicObjectsRegistry.Register(_currentRb);
    }

    private void StopCurrentPillow()
    {
        if (_currentRb == null) return;

        _currentRb.linearVelocity = Vector3.zero;
        _currentRb.angularVelocity = Vector3.zero;
        _currentRb.isKinematic = true;

        _currentRb = null;
    }

    private void HandleRecall()
    {
        if (_thrownPillows.Count == 0) return;

        GameObject pillowToRecall = _thrownPillows.Pop();
        Rigidbody rb = pillowToRecall.GetComponent<Rigidbody>();
        //_dynamicObjectsRegistry.Unregister(rb);
        StartCoroutine(RecallPillow(rb));
        _currentRb = null;
    }

    private IEnumerator RecallPillow(Rigidbody pillowRb)
    {
        pillowRb.isKinematic = true;
        pillowRb.detectCollisions = false;
        while (Vector3.Distance(pillowRb.position, _spawnPoint.position) > 0.1f)
        {
            pillowRb.position = Vector3.MoveTowards(pillowRb.position, _spawnPoint.position, _recallSpeed * Time.deltaTime);
            yield return null;
        }

        pillowRb.position = _spawnPoint.position;
        pillowRb.gameObject.SetActive(false);
    }
}
    //[SerializeField] private float _throwForse;
    //[SerializeField] private float _minForce = 5f;
    //[SerializeField] private float _maxForce = 15f;
    //[SerializeField] private float _chargeSpeed = 10f;
    //[SerializeField] protected float _returnSpeed;
    //[SerializeField] private Transform _pillow;
    //private Collider _collider;
    //private Rigidbody _rb;
    //private float _currentForce;
    //[SerializeField] private bool _charging;
    //[SerializeField] private bool _isThrowed = false;
    //[SerializeField] private bool _isMovingBack = false;

    //[SerializeField] private bool _isActive;

    //public override void Activate()
    //{
    //    base.Activate();
    //    _isActive = true;
    //    input.OnRecall += Recall;
    //    input.OnSecondaryFire += StopPillow;
    //}

    //public override void Deactivate()
    //{
    //    base.Deactivate();
    //    _isActive = false;
    //    input.OnRecall -= Recall;
    //    input.OnSecondaryFire -= StopPillow;
    //}

    //private void Awake()
    //{
    //    _rb = _pillow.gameObject.GetComponent<Rigidbody>();
    //    _collider = _pillow.gameObject.GetComponent<Collider>();
    //    _collider.enabled = false;
    //    _rb.isKinematic = true;
    //}

    //private void ChargeForce()
    //{
    //    if (input.IsFiringHeld())
    //    {
    //        _charging = true;
    //        _currentForce += _chargeSpeed * Time.deltaTime;
    //        _currentForce = Mathf.Clamp(_currentForce, _minForce, _maxForce);
    //        if (_isThrowed)
    //        {
    //            _isMovingBack = true;
    //            _charging = false;
    //            return;
    //        }
    //    }

    //    else if (_charging)
    //    {
    //        Throw();
    //        _charging = false;
    //        _currentForce = 0f;
    //    }
    //}

    //private void Throw()
    //{
    //    _collider.enabled = true;
    //    _isThrowed = true;
    //    _rb.isKinematic = false;
    //    Vector3 dir = _spawn.forward.normalized;
    //    _rb.AddForce(dir * _currentForce, ForceMode.Impulse);
    //    _pillow.parent = null;
    //}

    //protected override void Update()
    //{
    //    if (_isActive)
    //    {
    //        ChargeForce();
    //        Recall();
    //    }
    //}

    //private void Recall()
    //{
    //    if (_isMovingBack)
    //    {
    //        _pillow.transform.position = Vector3.MoveTowards(_pillow.transform.position, _spawn.position, _returnSpeed * Time.deltaTime);
    //        _rb.isKinematic = true;
    //        if (Vector3.Distance(_pillow.transform.position, _spawn.position) < 0.5f)
    //        {
    //            _pillow.transform.position = _spawn.position;
    //            _isMovingBack = false;
    //            _isThrowed = false;
    //            _collider.enabled = false;
    //            _charging = false;
    //            _pillow.parent = _spawn;
    //        }
    //    }
    //}

    //private void StopPillow()
    //{
    //    _rb.linearVelocity = Vector3.zero;
    //    _rb.isKinematic = true;
    //}
 
