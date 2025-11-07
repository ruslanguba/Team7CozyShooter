using UnityEngine;

public class Paintable : Damagable
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _paintedColor;
    private Material _objectMaterial;
    protected Color _color;
    [SerializeField] private float _steps = 5;
    private float _currentStep = 0;

    public override void TakeHit()
    {
        PaintStep();
    }

    private void Awake()
    {
        _steps = _maxHitPoints;
        _renderer = GetComponent<Renderer>();
        _objectMaterial = _renderer.material;
        _color = _startColor;
        _objectMaterial.color = _color;
        _renderer.material = _objectMaterial;
    }

    private void PaintStep()
    {
        if (_currentStep >= _steps)
            return;

        _currentStep++;

        float t = _currentStep / _steps;
        Color newColor = Color.Lerp(_startColor, _paintedColor, t);

        _objectMaterial.color = newColor;
    }
}
