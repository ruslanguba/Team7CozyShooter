using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    public PlayerSettings Settings => _settings;
    public InputReader Input => _inputReader;
    public CharacterController Controller => _controller;
    public Transform CameraPivot => _cameraPivot;
    public AnimatorHandler AnimatorHandler => _animatorHandler;

    [SerializeField] private PlayerSettings _settings;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimatorHandler _animatorHandler;

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
        _animator = GetComponentInChildren<Animator>();
        _animatorHandler = new AnimatorHandler(_animator);
        _movement = new PlayerMovement(this);
        _gravity = new PlayerGravity(this);
        _jump = new PlayerJump(this, _gravity);
        _look = new PlayerLook(this);
        //_movement = new PlayerMovement(_settings, _controller, _cameraPivot, _inputReader, _animator);
        //_gravity = new PlayerGravity(_settings, _controller);
        //_jump = new PlayerJump(_settings, _controller, _gravity, _inputReader);
        //_look = new PlayerLook(_settings, _cameraPivot, transform, _inputReader);
    }

    private void OnEnable()
    {
        _inputReader.OnJump += _jump.Jump;
    }
    private void OnDisable()
    {
        _inputReader.OnJump -= _jump.Jump;
    }
    private void Update()
    {
        _look.UpdateLook();
        _movement.UpdateMovement();
        _gravity.UpdateGravity();
        _movement.ApplyMovement(_gravity.VerticalVelocity);
    }
}
