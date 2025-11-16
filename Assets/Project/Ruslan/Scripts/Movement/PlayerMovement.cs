using UnityEngine;

public class PlayerMovement
{
    private PlayerSettings _settings;
    private CharacterController _controller;
    private Transform _cameraTransform;
    private InputReader _inputReader;
    private Animator _animator;

    private Vector3 _horizontalVelocity;
    private float _currentSpeed;
    private float _speedMultiplier = 1;

    public Vector3 HorizontalVelocity => _horizontalVelocity;

    public PlayerMovement(PlayerSettings settings, CharacterController controller, Transform cameraTransform, InputReader inputReader, Animator animator)
    {
        _settings = settings;
        _controller = controller;
        _cameraTransform = cameraTransform;
        _inputReader = inputReader;
        _animator = animator;
    }

    public void UpdateMovement()
    {
        Vector2 moveInput = _inputReader.GetMove();

        float targetSpeed = moveInput.sqrMagnitude > 0.0001f ? _settings.walkSpeed : 0f;
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, _settings.acceleration * Time.deltaTime);

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
        _animator.SetFloat("speed", _currentSpeed);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        _speedMultiplier = multiplier;
    }
}
