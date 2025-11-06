using UnityEngine;

public class PlayerLook
{
    private PlayerSettings _settings;
    private Transform _cameraPivot;
    private Transform _playerBody;
    private InputReader _inputReader;

    private Vector2 _currentLookDelta;
    private Vector2 _targetLookDelta;
    private Vector2 _lookVelocity;

    private float _pitch;
    private float _yaw;

    public PlayerLook(PlayerSettings settings, Transform cameraPivot, Transform playerBody, InputReader inputReader)
    {
        _settings = settings;
        _cameraPivot = cameraPivot;
        _playerBody = playerBody;
        _inputReader = inputReader;

        _yaw = _playerBody.eulerAngles.y;
        _pitch = _cameraPivot.localEulerAngles.x;
        if (_pitch > 180f) _pitch -= 360f;
    }

    public void UpdateLook()
    {
        _targetLookDelta = _inputReader.GetLook() * _settings.lookSensitivity;
        _currentLookDelta = Vector2.SmoothDamp(_currentLookDelta, _targetLookDelta, ref _lookVelocity, _settings.lookSmoothTime);

        _yaw += _currentLookDelta.x;
        _pitch -= _currentLookDelta.y;
        _pitch = Mathf.Clamp(_pitch, _settings.minPitch, _settings.maxPitch);

        _playerBody.rotation = Quaternion.Euler(0f, _yaw, 0f);
        _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}
