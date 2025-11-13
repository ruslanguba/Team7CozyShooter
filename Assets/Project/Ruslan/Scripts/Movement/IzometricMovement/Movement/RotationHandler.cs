using UnityEngine;
using UnityEngine.InputSystem;

public class RotationHandler : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float _rotationSpeed = 10f;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        if (Mouse.current == null) return; // защита для билда без мыши

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            Vector3 direction = hitPoint - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            }
        }
    }
}
