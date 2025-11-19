using UnityEngine;

public class PlayerMovement
{
    PlayerContext _context;
    private PlayerSettings _settings;
    private CharacterController _controller;
    private Transform _cameraTransform;
    private InputReader _inputReader;
    private AnimatorHandler _animator;

    private Vector3 _horizontalVelocity;
    private float _currentSpeed;
    private float _speedMultiplier = 1;

    public Vector3 HorizontalVelocity => _horizontalVelocity;

    public PlayerMovement(PlayerContext context)
    {
        _context = context;
        _settings = _context.Settings;
        _controller = _context.Controller;
        _cameraTransform = _context.CameraPivot;
        _inputReader = _context.Input;
        _animator = _context.AnimatorHandler;
    }

    public void UpdateMovement()
    {
        Vector2 moveInput = _inputReader.GetMove();

        float targetSpeed = moveInput.sqrMagnitude > 0.0001f ? _settings.walkSpeed : 0f;
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, _settings.acceleration * Time.deltaTime);

        SetAnimatorParams(moveInput);

        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        _horizontalVelocity = (forward * moveInput.y + right * moveInput.x).normalized * _currentSpeed * _speedMultiplier;
    }

    public void ApplyMovement(Vector3 verticalVelocity)
    {
        _controller.Move((_horizontalVelocity + verticalVelocity) * Time.deltaTime);
    }

    private void SetAnimatorParams(Vector2 moveInput)
    {
        bool isMoving = moveInput.magnitude > 0.05f; // движение / Idle
        int moveDir = 0;
        float multiplier = 1f;

        if (isMoving)
        {
            bool forward = moveInput.y > 0.05f;
            bool backward = moveInput.y < -0.05f;
            bool right = moveInput.x > 0.05f;
            bool left = moveInput.x < -0.05f;

            if (forward)
            {
                if (right) moveDir = 2;   // вперед+вправо
                else if (left) moveDir = -2; // вперед+влево
                else moveDir = 1;          // только вперед
                multiplier = 1f;
            }
            else if (backward)
            {
                if (right) moveDir = -2;    // назад+вправо - воспроизводим влево задом
                else if (left) moveDir = 2; // назад+влево - воспроизводим вправо задом
                else moveDir = 1;            // только назад
                multiplier = -1f;
            }
            else
            {
                // чисто боковое движение
                if (right) moveDir = 2;
                else if (left) moveDir = -2;
                multiplier = 1f;
            }
        }

        // --- Передаем параметры в AnimatorHandler ---
        _animator.SetSpeed(_currentSpeed);
        _animator.SetMoving(isMoving);
        _animator.SetMoveDirection(moveDir);
        _animator.SetAnimSpeedMultiplier(multiplier);
    }
}
