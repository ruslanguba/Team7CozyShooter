using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private CharacterController _characterController;
    private InputHandler _inputHandler;
    private GravityHandler _gravityHandler;

    private Vector3 _moveDirection; 

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputHandler = GetComponent<InputHandler>();
        _gravityHandler = GetComponent<GravityHandler>();
    }

    void Update()
    {
        GetInputMoveVector();
        MoveCharacter();
    }

    private void GetInputMoveVector()
    {
        _moveDirection = _inputHandler.GetMove();
    }

    private void MoveCharacter()
    {
       Vector3 velocity =
       new Vector3(_moveDirection.x * _moveSpeed,
                   _gravityHandler.GetYVelocity(),
                   _moveDirection.y * _moveSpeed);

        _characterController.Move(velocity * Time.deltaTime);
    }
}
