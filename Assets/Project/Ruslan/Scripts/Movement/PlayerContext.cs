using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    [SerializeField] private PlayerSettings _settings;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Animator _animator;
    public PlayerGravity PlayerGravity => _gravity;
    public PlayerMovement PlayerMovement => _movement;

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
        _animator = GetComponent<Animator>();
        _movement = new PlayerMovement(_settings, _controller, _cameraPivot, _inputReader, _animator);
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
}
