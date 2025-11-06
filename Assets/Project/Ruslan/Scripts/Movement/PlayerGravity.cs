using UnityEngine;

public class PlayerGravity
{
    private PlayerSettings _settings;
    private CharacterController _controller;
    private Vector3 _velocity;

    public Vector3 VerticalVelocity => _velocity;

    public PlayerGravity(PlayerSettings settings, CharacterController controller)
    {
        _settings = settings;
        _controller = controller;
    }

    public void UpdateGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0f)
            _velocity.y = -2f;

        _velocity.y += _settings.gravity * Time.deltaTime;
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
