using Unity.Cinemachine;
using UnityEngine;

public class PlayerLook
{
    private PlayerContext _context;
    private PlayerSettings _settings;
    private Transform _bodyTransform;
    private CinemachineOrbitalFollow _orbitalFollow;
    private CinemachineCamera _cineCam;
    private InputReader _inputReader;

    private Vector2 _currentLookDelta;
    private Vector2 _lookVelocity;

    public PlayerLook(PlayerContext context, CinemachineCamera cineCam, CinemachineOrbitalFollow orbitalFollow)
    {
        _context = context;
        _settings = _context.Settings;
        _bodyTransform = _context.BodyTransform;
        _inputReader = _context.Input;

        _cineCam = cineCam;
        _orbitalFollow = orbitalFollow;
    }

    public void UpdateLook()
    {
        Vector2 lookDelta = _inputReader.GetLook();

        _currentLookDelta = Vector2.SmoothDamp(
            _currentLookDelta,
            lookDelta,
            ref _lookVelocity,
            _settings.lookSmoothTime
        );

        if (_inputReader.IsAimingHeld())
            AimMode();
    }

    private void AimMode()
    {
        // Берём yaw камеры
        float cameraYaw = _cineCam.State.RawOrientation.eulerAngles.y;

        // Текущий yaw персонажа
        float currentYaw = _bodyTransform.eulerAngles.y;

        // Плавно приближаем персонажа к yaw камеры
        float newYaw = Mathf.LerpAngle(
            currentYaw,
            cameraYaw,
            _settings.rotationSpeed * Time.deltaTime
        );

        // Применяем
        _bodyTransform.rotation = Quaternion.Euler(0f, newYaw, 0f);
    }
}
