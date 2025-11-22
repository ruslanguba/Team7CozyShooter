using Unity.VisualScripting;
using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    public PlayerSettings Settings => _settings;
    public InputReader Input => _inputReader;
    public CharacterController Controller => _controller;
    public Transform CameraPivot => _cameraPivot;
    public AnimatorHandler AnimatorHandler => _animatorHandler;
    public ActorAudio Audio => _audio;

    [SerializeField] private PlayerSettings _settings;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _cameraPivot;
    private Animator _animator;
    private AnimatorHandler _animatorHandler;
    private ActorAudio _audio;

    private CharacterController _controller;
    private PlayerMovement _movement;
    private PlayerGravity _gravity;
    private PlayerJump _jump;
    private PlayerLook _look;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputReader = GetComponent<InputReader>();
        _animator = GetComponentInChildren<Animator>();
        _audio = GetComponent<ActorAudio>();
        _animatorHandler = new AnimatorHandler(_animator);
        _movement = new PlayerMovement(this);
        _gravity = new PlayerGravity(this);
        _jump = new PlayerJump(this, _gravity);
        _look = new PlayerLook(this);
    }

    private void OnEnable()
    {
        //PlayerManager.Instance.GetPlayerTransform(transform);
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
