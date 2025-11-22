using UnityEngine;

public class PlayerJump
{
    private PlayerContext _context;
    private PlayerSettings _settings;
    private PlayerGravity _gravity;
    private CharacterController _controller;
    private InputReader _inputReader;
    private AnimatorHandler _animator;

    public PlayerJump(PlayerContext context, PlayerGravity gravity)
    {
        _context = context;
        _settings = _context.Settings;
        _controller = _context.Controller;
        _gravity = gravity;
        _inputReader = _context.Input;
        _animator = _context.AnimatorHandler;
    }

    public void Jump()
    {
        if (_controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(_settings.jumpHeight * -2f * _settings.gravity);
            _gravity.SetYVelocity(jumpVelocity);
            _animator.SetJump();  // включаем анимацию прыжка
            _context.Audio.PlayJump();
        }
    }
}
