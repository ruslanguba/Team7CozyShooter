using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action JumpAction;

    private PlayerInput inputActions;

    private void Awake()
    {
        inputActions = new PlayerInput();
        inputActions.Enable();
    }

    private void OnEnable()
    {
        if (inputActions != null)
        {
            SubscribeEvents();
        }
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        inputActions.Gameplay.Jump.performed += JumpPerformed;
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        JumpAction?.Invoke();
    }

    private void UnsubscribeEvents()
    {
        inputActions.Gameplay.Jump.performed -= JumpPerformed;
    }

    public Vector2 GetMove() => inputActions.Gameplay.Move != null ? inputActions.Gameplay.Move.ReadValue<Vector2>() : Vector2.zero;
    public Vector2 GetLook() => inputActions.Gameplay.Look.ReadValue<Vector2>();
}
