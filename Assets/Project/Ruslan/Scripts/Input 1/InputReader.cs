using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _lookAction;
    [SerializeField] private InputActionReference _jumpAction;

    [SerializeField] private InputActionReference _fireAction;
    [SerializeField] private InputActionReference _secondaryFire;
    [SerializeField] private InputActionReference _recallAction;
    [SerializeField] private InputActionReference _throwAction;
    [SerializeField] private InputActionReference _interractAction;
    [SerializeField] private InputActionReference _scrollAction;
    [SerializeField] private InputActionReference _pauseAction;

    // ќдноразовые событи€
    public event Action OnJump;
    public event Action OnFire;
    public event Action OnSecondaryFire;
    public event Action OnFireRealesed;
    public event Action OnSecondaryFireRealesed;
    public event Action OnRecall;
    public event Action OnThrow;
    public event Action OnInteract;
    public event Action OnPause;
    public event Action<float> OnScroll;

    private void OnEnable()
    {
        EnableAllActions();
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
        DisableAllActions();
    }

    private void EnableAllActions()
    {
        _moveAction?.action?.Enable();
        _lookAction?.action?.Enable();
        _jumpAction?.action?.Enable();
        _fireAction?.action?.Enable();
        _secondaryFire?.action?.Enable();
        _recallAction?.action?.Enable();
        _throwAction?.action?.Enable();
        _interractAction?.action?.Enable();
        _scrollAction?.action?.Enable();
        _pauseAction?.action?.Enable();
    }

    private void DisableAllActions()
    {
        _moveAction?.action?.Disable();
        _lookAction?.action?.Disable();
        _jumpAction?.action?.Disable();
        _fireAction?.action?.Disable();
        _secondaryFire?.action?.Disable();
        _recallAction?.action?.Disable();
        _throwAction?.action?.Disable();
        _interractAction?.action?.Disable();
        _scrollAction?.action?.Disable();
        _pauseAction?.action?.Disable();
    }

    private void SubscribeEvents()
    {
        if (_fireAction?.action != null)
        {
            _fireAction.action.performed += ctx => OnFire?.Invoke();
            _fireAction.action.canceled += ctx => OnFireRealesed?.Invoke();
        }
        if (_secondaryFire?.action != null)
        {
            _secondaryFire.action.performed += ctx => OnSecondaryFire?.Invoke();
            _secondaryFire.action.canceled += ctx => OnSecondaryFireRealesed?.Invoke();
        }
        if (_jumpAction?.action != null) _jumpAction.action.performed += ctx => OnJump?.Invoke();
        if (_recallAction?.action != null) _recallAction.action.performed += ctx => OnRecall?.Invoke();
        if (_throwAction?.action != null) _throwAction.action.performed += ctx => OnThrow?.Invoke();
        if (_interractAction?.action != null) _interractAction.action.performed += ctx => OnInteract?.Invoke();
        if (_pauseAction?.action != null) _pauseAction.action.performed += ctx => OnPause?.Invoke();
        if (_scrollAction?.action != null) _scrollAction.action.performed += ctx => OnScroll?.Invoke(ctx.ReadValue<Vector2>().y);

    }

    private void UnsubscribeEvents()
    {
        if (_fireAction?.action != null)
        {
            _fireAction.action.performed -= ctx => OnFire?.Invoke();
            _fireAction.action.canceled -= ctx => OnFireRealesed?.Invoke();
        }
        if (_secondaryFire?.action != null)
        {
            _secondaryFire.action.performed -= ctx => OnSecondaryFire?.Invoke();
            _secondaryFire.action.canceled -= ctx => OnSecondaryFireRealesed?.Invoke();
        }
        if (_jumpAction?.action != null) _jumpAction.action.performed -= ctx => OnJump?.Invoke();
        if (_recallAction?.action != null) _recallAction.action.performed -= ctx => OnRecall?.Invoke();
        if (_throwAction?.action != null) _throwAction.action.performed -= ctx => OnThrow?.Invoke();
        if (_interractAction?.action != null) _interractAction.action.performed -= ctx => OnInteract?.Invoke();
        if (_pauseAction?.action != null) _pauseAction.action.performed -= ctx => OnPause?.Invoke();
        if (_scrollAction?.action != null) _scrollAction.action.performed -= ctx => OnScroll?.Invoke(ctx.ReadValue<Vector2>().y);
    }

    // Pull дл€ удержани€ и перемещени€
    public Vector2 GetMove() => _moveAction?.action != null ? _moveAction.action.ReadValue<Vector2>() : Vector2.zero;
    public Vector2 GetLook() => _lookAction?.action != null ? _lookAction.action.ReadValue<Vector2>() : Vector2.zero;
    public bool IsFiringHeld() => _fireAction?.action != null && _fireAction.action.IsPressed();
    public bool IsAimingHeld() => _secondaryFire?.action != null && _secondaryFire.action.IsPressed();
}
