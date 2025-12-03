using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    public PlayerSettings Settings => _settings;
    public InputReader Input => _inputReader;
    public CharacterController Controller => _controller;
    public Transform CameraPivot1 => _cameraPivot;
    public Transform BodyTransform => transform;
    public AnimatorHandler AnimatorHandler => _animatorHandler;
    public ActorAudio Audio => _audio;
    public Animator Animator => _animator;

    [SerializeField] private PlayerSettings _settings;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private CinemachineCamera _cinemachineCamera;
    [SerializeField] private CinemachineOrbitalFollow _orbitalFollow;
    private Animator _animator;
    private AnimatorHandler _animatorHandler;
    private ActorAudio _audio;

    [SerializeField] private PlayerRotationHandler _playerRotationHandler;
    private CharacterController _controller;
    private PlayerMovement _movement;
    private PlayerGravity _gravity;
    private PlayerJump _jump;
    private PlayerLook _look;

    public void SetAnimator(Animator animator)
    {
        _animator = animator;
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputReader = GetComponent<InputReader>();
        _animator = GetComponentInChildren<Animator>();
        _audio = GetComponent<ActorAudio>();
        _playerRotationHandler = GetComponent<PlayerRotationHandler>();
        _cinemachineCamera = GetComponentInChildren<CinemachineCamera>();
        _orbitalFollow = GetComponentInChildren<CinemachineOrbitalFollow>();
        _animatorHandler = new AnimatorHandler(this);
        _movement = new PlayerMovement(this, Camera.main, _playerRotationHandler);
        _gravity = new PlayerGravity(this);
        _jump = new PlayerJump(this, _gravity);
        _look = new PlayerLook(this, _cinemachineCamera, _orbitalFollow);
    }

    private void OnEnable()
    {
        _inputReader.OnJump += _jump.Jump;
        //_inputReader.OnScroll += _look.Zoom;
    }
    private void OnDisable()
    {
        _inputReader.OnJump -= _jump.Jump;
        //_inputReader.OnScroll += _look.Zoom;
    }
    private void Update()
    {
        _look.UpdateLook();
        _movement.UpdateMovement();
        _gravity.UpdateGravity();
        _movement.ApplyMovement(_gravity.VerticalVelocity);
    }
}
