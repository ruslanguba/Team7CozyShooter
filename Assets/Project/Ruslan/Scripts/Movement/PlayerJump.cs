using UnityEngine;

public class PlayerJump
{
    private PlayerSettings _settings;
    private PlayerGravity _gravity;
    private CharacterController _controller;
    private InputReader _inputReader;

    public PlayerJump(PlayerSettings settings, CharacterController controller, PlayerGravity gravity, InputReader inputReader)
    {
        _settings = settings;
        _controller = controller;
        _gravity = gravity;
        _inputReader = inputReader;
    }

    public void Jump()
    {
        if (_controller.isGrounded)
        {
            _gravity.SetYVelocity(Mathf.Sqrt(_settings.jumpHeight * -2f * _settings.gravity));
        }

    }
}
