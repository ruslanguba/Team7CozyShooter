using UnityEngine;

public class PlatformParenter : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private Transform _checkSurfacePivot;
    [SerializeField] private float radius = 0.3f;

    private RotationObject _currentPlatform;

    private void Update()
    {
        DetectPlatform();
    }

    public void DetectPlatform()
    {
        Collider[] hits = Physics.OverlapSphere(_checkSurfacePivot.position, radius);

        RotationObject newPlatform = null;

        foreach (var hit in hits)
        {
            var rot = hit.GetComponent<RotationObject>();
            if (rot != null)
            {
                newPlatform = rot;
                break;
            }
        }

        // зашли на платформу
        if (newPlatform != null)
            _currentPlatform = newPlatform;

        // ушли
        else
            _currentPlatform = null;
    }
    public bool IsOnPlatform()
    {
        return _currentPlatform != null;
    }
}
