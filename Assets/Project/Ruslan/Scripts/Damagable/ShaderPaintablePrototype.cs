using System.Collections;
using UnityEngine;

public class ShaderPaintablePrototype : Damagable
{
    string _transientKey = "_Transient";

    [SerializeField] Renderer _renderer;

    [SerializeField] private float _steps = 5;
    private float _currentStep = 0;

    private void Awake()
    {
        _renderer.material = new Material(_renderer.material);

        // Если ты хочешь, чтобы количество шагов зависело от хитпоинтов:
        _steps = _maxHitPoints;
    }

    public override void TakeHit()
    {
        PaintStep();
    }

    private void PaintStep()
    {
        if (_currentStep >= _steps)
            return;

        _currentStep++;

        float t = _currentStep / _steps;
        _renderer.material.SetFloat(_transientKey, t * 2);
    }
}
