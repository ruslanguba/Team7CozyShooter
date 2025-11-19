using UnityEngine;

public class PlayerGravity
{
    PlayerContext _context;
    private PlayerSettings _settings;
    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isMovingUp;

    public Vector3 VerticalVelocity => _velocity;

    public PlayerGravity(PlayerContext context)
    {
        _context = context;
        _settings = _context.Settings;
        _controller = _context.Controller;
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
