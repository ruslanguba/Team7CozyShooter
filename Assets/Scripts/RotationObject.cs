using UnityEngine;

public class RotationObject : MonoBehaviour
{
    [SerializeField] private float _speedRotationX;
    [SerializeField] private float _speedRotationY;
    [SerializeField] private float _speedRotationZ;

    public Quaternion DeltaRot { get; private set; }

    private Quaternion _lastRotation;

    void Start()
    {
        _lastRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // вычисляем разницу между прошлой и новой ротацией
        DeltaRot = transform.rotation * Quaternion.Inverse(_lastRotation);

        _lastRotation = transform.rotation;
    }

    void Update()
    {

        transform.Rotate(_speedRotationX * Time.deltaTime, _speedRotationY * Time.deltaTime, _speedRotationZ * Time.deltaTime);
    }
}
