using Unity.Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _minZoom;
    [SerializeField] private float _maxZoom;
    [SerializeField] private float _zoomSensitivity;
    [SerializeField] private float _currentZoom;
    [SerializeField] private CinemachineOrbitalFollow _orbitalFollow;
    [SerializeField] private InputReader _inputReader;


    private void OnEnable()
    {
        _orbitalFollow.GetComponentInChildren<CinemachineOrbitalFollow>();
        _inputReader = GetComponent<InputReader>();
        _inputReader.OnScroll += Zoom;
        _currentZoom = _orbitalFollow.Radius;
    }
    private void OnDisable()
    {
        _inputReader.OnScroll -= Zoom;
    }

    public void Zoom(float zoom)
    {
        float newZoom = _currentZoom - zoom * _zoomSensitivity;
        newZoom = Mathf.Clamp(newZoom, _minZoom, _maxZoom);

        _currentZoom = newZoom;
        _orbitalFollow.Radius = _currentZoom;
    }
}
