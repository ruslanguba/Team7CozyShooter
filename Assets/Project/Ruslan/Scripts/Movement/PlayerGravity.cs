using UnityEngine;

public class PlayerGravity
{
    private PlayerSettings _settings;
    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isMovingUp;

    public Vector3 VerticalVelocity => _velocity;

    public PlayerGravity(PlayerSettings settings, CharacterController controller)
    {
        _settings = settings;
        _controller = controller;
    }

    public void UpdateGravity()
    {
        if (_isMovingUp) 
            return;
        if (_controller.isGrounded && _velocity.y < 0f)
            _velocity.y = -2f;

        _velocity.y += _settings.gravity * Time.deltaTime;
    }
    public void SetMoveUpwords(bool isMovigUp)
    {
        _isMovingUp = isMovigUp;
    }

    public void SetYVelocity(float value)
    {
        _velocity.y = value;
    }

    public float GetYVelocity()
    {
        return _velocity.y;
    }
}
