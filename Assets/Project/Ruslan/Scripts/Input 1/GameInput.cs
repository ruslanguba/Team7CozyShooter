using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    [SerializeField] private InputActionReference _pauseAction;
    [SerializeField] private InputActionReference _tubAction;

    public event Action OnPause;
    public event Action OnTub;

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
        _pauseAction?.action?.Enable();
        _tubAction?.action?.Enable();
    }

    private void DisableAllActions()
    {
        _pauseAction?.action?.Disable();
        _tubAction?.action?.Disable();
    }

    private void SubscribeEvents()
    {
        if (_pauseAction?.action != null) _pauseAction.action.performed += ctx => OnPause?.Invoke();
        if (_tubAction?.action != null) _tubAction.action.performed += ctx => OnTub?.Invoke();
    }

    private void UnsubscribeEvents()
    {
        if (_pauseAction?.action != null) _pauseAction.action.performed -= ctx => OnPause?.Invoke();
        if (_tubAction?.action != null) _tubAction.action.performed -= ctx => OnTub?.Invoke();
    }
}
