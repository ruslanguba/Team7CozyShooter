using UnityEngine;
using UnityEngine.LowLevel;

public class PlayerContext : MonoBehaviour
{
    [SerializeField] private PlayerSettings _settings;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _cameraPivot;

    private CharacterController _controller;
    private PlayerMovement _movement;
    private PlayerGravity _gravity;
    private PlayerJump _jump;
    private PlayerLook _look;

    void Start()
    {
        PlayerManager.CurrentPlayer = transform;
    }

    private void Awake()
    {
        
        _controller = GetComponent<CharacterController>();
        _inputReader = GetComponent<InputReader>();
        _movement = new PlayerMovement(_settings, _controller, _cameraPivot, _inputReader);
        _gravity = new PlayerGravity(_settings, _controller);
        _jump = new PlayerJump(_settings, _controller, _gravity, _inputReader);
        _look = new PlayerLook(_settings, _cameraPivot, transform, _inputReader);
    }

    private void OnEnable()
    {
        _inputReader.OnJump += _jump.Jump;
    }
    private void OnDisable()
    {
        _inputReader.OnJump += _jump.Jump;
    }
    private void Update()
    {
        _look.UpdateLook();
        _movement.UpdateMovement();
        _gravity.UpdateGravity();
        _movement.ApplyMovement(_gravity.VerticalVelocity);
    }

    private void OnSpeedChanged(bool isAiming)
    {
        // Меняем скорость движения при прицеливании
        _movement.SetSpeedMultiplier(isAiming ? 0.5f : 1f); // например, половина скорости
    }
}
