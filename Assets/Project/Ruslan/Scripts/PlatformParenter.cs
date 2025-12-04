using UnityEngine;

public class PlatformParenter : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private Transform _checkSurfacePivot;
    [SerializeField] private float radius = 0.3f;

    private CharacterController _cc;
    private RotationObject _currentPlatform;

    private void Start()
    {
        _cc = _character.GetComponent<CharacterController>();
    }

    private void Update()
    {
        DetectPlatform();
        MoveWithPlatform();
    }

    void DetectPlatform()
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

    void MoveWithPlatform()
    {
        if (_currentPlatform == null)
            return;

        // позиция игрока локально относительно платформы
        Vector3 local = _currentPlatform.transform.InverseTransformPoint(_character.position);

        // применяем вращение
        local = _currentPlatform.DeltaRot * local;

        // возвращаем в мировые координаты
        Vector3 newWorldPos = _currentPlatform.transform.TransformPoint(local);

        // CharacterController не даёт менять transform, поэтому его нужно отключить
        _cc.enabled = false;
        _character.position = newWorldPos;
        _cc.enabled = true;

        // поворачиваем игрока вместе с платформой
        _character.rotation = _currentPlatform.DeltaRot * _character.rotation;
    }

    public bool IsOnPlatform()
    {
        return _currentPlatform != null;
    }
}
