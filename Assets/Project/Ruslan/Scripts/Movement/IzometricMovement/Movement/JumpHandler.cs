using UnityEngine;

public class JumpHandler : MonoBehaviour
{
    [SerializeField] private float _jumpHeight;

    private InputReader _inputHandler;
    private GravityHandler _gravityHandler;
    private CharacterController _characterController;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputHandler = GetComponent<InputReader>();
        _gravityHandler = GetComponent<GravityHandler>();
    }

    private void OnEnable()
    {
        _inputHandler.OnJump += OnJumpAction;
    }
    private void OnDisable()
    {
        _inputHandler.OnJump -= OnJumpAction;
    
    }

    private void OnJumpAction()
    {
        if (_characterController.isGrounded)
        {
            float jumpVel = Mathf.Sqrt(2f * Mathf.Abs(_gravityHandler.GetGravity) * _jumpHeight);
            _gravityHandler.SetYVelocity(jumpVel);
        }
    }
}
