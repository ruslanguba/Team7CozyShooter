using UnityEngine;

public class PlayerMovement
{
    PlayerContext _context;
    private PlayerSettings _settings;
    private CharacterController _controller;
    private Transform _bodyTransform;
    private Transform _cameraTransform;
    private InputReader _inputReader;
    private AnimatorHandler _animator;
    private PlayerRotationHandler _playerRotationHandler;

    private Vector3 _horizontalVelocity;
    private float _currentSpeed;
    private float _speedMultiplier = 1;

    public Vector3 HorizontalVelocity => _horizontalVelocity;

    public PlayerMovement(PlayerContext context, Camera camera, PlayerRotationHandler rotationHandler)
    {
        _context = context;
        _settings = _context.Settings;
        _controller = _context.Controller;
        _bodyTransform = _context.BodyTransform;
        _cameraTransform = camera.transform;
        _inputReader = _context.Input;
        _animator = _context.AnimatorHandler;
        _playerRotationHandler = rotationHandler;
    }

    public void UpdateMovement()
    {
        Vector2 moveInput = _inputReader.GetMove();

        float targetSpeed = moveInput.sqrMagnitude > 0.0001f ? _settings.walkSpeed : 0f;
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, _settings.acceleration * Time.deltaTime);

        SetAnimatorParams(moveInput);
        PlayFootstepsSound(_currentSpeed > 0);

        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _bodyTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 camToPlayer = _bodyTransform.position - _cameraTransform.position;
        camToPlayer.y = 0f;
        Vector3 moveDir = camToPlayer.normalized;

        if (moveDir.sqrMagnitude > 0.01f && Mathf.Abs(moveInput.y) > 0.05f)
        {
            _playerRotationHandler.UpdateRotation(moveDir.normalized, _context.Settings.rotationSpeed);
        }

        SetAnimatorParams(moveInput);
        PlayFootstepsSound(_currentSpeed > 0);
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

    private void PlayFootstepsSound(bool isMoving)
    {
        bool isFoottSoundPlay = isMoving && _context.Controller.isGrounded;
        _context.Audio.TickFootsteps(isFoottSoundPlay);
    }
}
