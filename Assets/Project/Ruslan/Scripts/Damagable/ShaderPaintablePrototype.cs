using System.Collections;
using UnityEngine;

public class ShaderPaintablePrototype : InteractableBase
{
    string _transientKey = "_Transient";

    [SerializeField] Renderer[] _renderer;
    [SerializeField] private Material _colorChangeMaterial;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _steps = 5;
    [SerializeField] private float _maxHitPoints = 5;
    private float _currentStep = 0;

    private ScoreManager _scoreManager;
    private void Awake()
    {
        foreach (var renderer in _renderer)
        {
            renderer.material = _colorChangeMaterial;
            renderer.material = new Material(renderer.material);
        }

        if (TryGetComponent(out Collider collider))
        {
            _collider = collider;
        }
        else
        {
            _collider = GetComponentInChildren<Collider>();
        }
        if(_scoreManager == null)
        {
            _scoreManager = FindFirstObjectByType<ScoreManager>();
        }

        // Если ты хочешь, чтобы количество шагов зависело от хитпоинтов:
        _steps = _maxHitPoints;
    }

    public override void OnInteract()
    {
        PaintStep();
    }

    private void PaintStep()
    {
        if (_currentStep >= _steps)
            return;

        _currentStep++;
        if(_currentStep <= _steps)
        {
            _scoreManager.AddScore(10);
        }
        float t = _currentStep / _steps;
        foreach (var renderer in _renderer)
        {
            renderer.material.SetFloat(_transientKey, t * 2);
        }
    }
}
