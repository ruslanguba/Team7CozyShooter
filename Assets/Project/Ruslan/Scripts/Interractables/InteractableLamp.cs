using UnityEngine;

public class InteractableLamp : InteractableBase
{
    private Light _light;
    private bool _enabled;
    
    void Start()
    {
        _light = GetComponentInChildren<Light>();
        _enabled = _light.enabled;
    }

    public override void OnInteract()
    {
        SwichLight();
    }

    private void SwichLight()
    {
        _enabled = !_enabled;
        _light.enabled = _enabled;
    }
}
