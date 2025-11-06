using UnityEngine;
using UnityEngine.InputSystem;

public class RotationHandler : MonoBehaviour
{
    private Camera cam;
    private InputHandler input;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;
        input = GetComponent<InputHandler>();
    }

    private void Update()
    {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        Ray ray = cam.ScreenPointToRay(input.GetLook());

        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            Vector3 direction = hitPoint - transform.position;
            direction.y = 0f; // чтобы не заваливало вверх/вниз

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = targetRot;
            }
        }
    }
}
